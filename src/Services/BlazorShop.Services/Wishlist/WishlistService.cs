namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using DateTime;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Web.Shared.Products;

    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext db;
        private readonly IDateTimeProvider dataProvider;

        public WishlistService(
            ApplicationDbContext db,
            IDateTimeProvider dataProvider)
        {
            this.db = db;
            this.dataProvider = dataProvider;
        }

        public async Task AddProductAsync(int productId, string userId)
        {
            var wishlist = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    ProductId = productId,
                    UserId = userId,
                    CreatedOn = this.dataProvider.Now()
                };

                await this.db.Wishlists.AddAsync(wishlist);
            }
            else
            {
                wishlist.IsDeleted = false;
                wishlist.DeletedOn = null;
                wishlist.ModifiedOn = this.dataProvider.Now();
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> RemoveProductAsync(int productId, string userId)
        {
            var wishlist = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (wishlist == null)
            {
                return false;
            }

            wishlist.IsDeleted = true;
            wishlist.DeletedOn = this.dataProvider.Now();

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> GetByUserIdAsync(string userId)
            => await this.db
                .Wishlists
                .Where(w => w.UserId == userId && !w.IsDeleted)
                .Select(w => w.Product)
                .To<ProductsListingResponseModel>()
                .ToListAsync();

        private async Task<Wishlist> GetByProductIdAndUserIdAsync(int productId, string userId)
            => await this.db
                .Wishlists
                .FirstOrDefaultAsync(w => w.ProductId == productId && w.UserId == userId);
    }
}
