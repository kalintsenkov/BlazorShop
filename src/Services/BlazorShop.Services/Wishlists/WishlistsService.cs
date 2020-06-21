namespace BlazorShop.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Products;

    public class WishlistsService : BaseService<Wishlist>, IWishlistsService
    {
        public WishlistsService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task AddAsync(int productId, string userId)
        {
            var wishlist = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    ProductId = productId
                };

                await this.Data.Wishlists.AddAsync(wishlist);
            }
            else
            {
                wishlist.IsDeleted = false;
                wishlist.DeletedOn = null;
            }

            await this.Data.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int productId, string userId)
        {
            var wishlist = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (wishlist == null)
            {
                return false;
            }

            this.Data.Remove(wishlist);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> GetByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllByUserId(userId)
                    .AsNoTracking()
                    .Select(w => w.Product))
                .ToListAsync();

        private async Task<Wishlist> GetByProductIdAndUserIdAsync(int productId, string userId)
            => await this
                .AllByUserId(userId, withDeleted: true)
                .FirstOrDefaultAsync(w => w.ProductId == productId);

        private IQueryable<Wishlist> AllByUserId(string userId, bool withDeleted = false)
            => withDeleted == true
                ? this.All().Where(w => w.UserId == userId)
                : this.All().Where(w => w.UserId == userId && !w.IsDeleted);
    }
}
