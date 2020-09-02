namespace BlazorShop.Services.WishList
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models;
    using Models.Products;

    public class WishListService : BaseService<WishList>, IWishListService
    {
        public WishListService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<Result> AddProductAsync(int productId, string userId)
        {
            var wishList = await this.GetByProductIdAndUserIdAsync(productId, userId);

            if (wishList == null)
            {
                wishList = new WishList
                {
                    UserId = userId,
                    ProductId = productId
                };

                await this.Data.AddAsync(wishList);
            }
            else
            {
                wishList.DeletedOn = null;
                wishList.IsDeleted = false;
            }

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> RemoveProductAsync(int productId, string userId)
        {
            var wishList = await this.GetByProductIdAndUserIdAsync(productId, userId);

            if (wishList == null)
            {
                return "This user cannot delete products from this wish list.";
            }

            this.Data.Remove(wishList);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> ByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllByUserId(userId)
                    .AsNoTracking()
                    .Select(w => w.Product))
                .ToListAsync();

        private async Task<WishList> GetByProductIdAndUserIdAsync(int productId, string userId)
            => await this
                .AllByUserId(userId, withDeleted: true)
                .FirstOrDefaultAsync(w => w.ProductId == productId);

        private IQueryable<WishList> AllByUserId(string userId, bool withDeleted = false)
            => withDeleted
                ? this.All().Where(w => w.UserId == userId)
                : this.All().Where(w => w.UserId == userId && !w.IsDeleted);
    }
}
