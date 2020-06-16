namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Products;

    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public WishlistService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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

                await this.db.Wishlists.AddAsync(wishlist);
            }
            else
            {
                wishlist.IsDeleted = false;
                wishlist.DeletedOn = null;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int productId, string userId)
        {
            var wishlist = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (wishlist == null)
            {
                return false;
            }

            this.db.Remove(wishlist);

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> GetByUserIdAsync(string userId)
            => await this
                .AllByUserId(userId)
                .AsNoTracking()
                .Select(w => w.Product)
                .ProjectTo<ProductsListingResponseModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        private async Task<Wishlist> GetByProductIdAndUserIdAsync(int productId, string userId)
            => await this.db
                .Wishlists
                .FirstOrDefaultAsync(w => w.ProductId == productId && w.UserId == userId);

        private IQueryable<Wishlist> AllByUserId(string userId)
            => this.db
                .Wishlists
                .Where(w => w.UserId == userId && !w.IsDeleted);
    }
}
