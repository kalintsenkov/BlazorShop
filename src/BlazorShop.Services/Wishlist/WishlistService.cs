namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Identity;
    using Models;
    using Models.Products;

    public class WishlistService : BaseService<Wishlist>, IWishlistService
    {
        private readonly ICurrentUserService currentUser;

        public WishlistService(
            ApplicationDbContext db,
            IMapper mapper,
            ICurrentUserService currentUser)
            : base(db, mapper)
            => this.currentUser = currentUser;

        public async Task<Result> AddProductAsync(int productId)
        {
            var wishlist = await this.GetByProductAndUserAsync(productId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = this.currentUser.UserId,
                    ProductId = productId
                };

                await this.Data.AddAsync(wishlist);
            }
            else
            {
                wishlist.DeletedOn = null;
                wishlist.IsDeleted = false;
            }

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> RemoveProductAsync(int productId)
        {
            var wishlist = await this.GetByProductAndUserAsync(productId);

            if (wishlist == null)
            {
                return "This user cannot delete products from this wish list.";
            }

            this.Data.Remove(wishlist);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> MineAsync()
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllByCurrentUser()
                    .AsNoTracking()
                    .Select(w => w.Product))
                .ToListAsync();

        private async Task<Wishlist> GetByProductAndUserAsync(
            int productId)
            => await this
                .AllByCurrentUser(withDeleted: true)
                .FirstOrDefaultAsync(w => w.ProductId == productId);

        private IQueryable<Wishlist> AllByCurrentUser(
            bool withDeleted = false)
            => withDeleted
                ? this.All().Where(w => w.UserId == this.currentUser.UserId)
                : this.All().Where(w => w.UserId == this.currentUser.UserId && !w.IsDeleted);
    }
}
