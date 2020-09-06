namespace BlazorShop.Services.ShoppingCart
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
    using Models.ShoppingCarts;

    public class ShoppingCartService : BaseService<ShoppingCart>, IShoppingCartService
    {
        private readonly ICurrentUserService currentUser;

        public ShoppingCartService(
            ApplicationDbContext db,
            IMapper mapper,
            ICurrentUserService currentUser)
            : base(db, mapper)
            => this.currentUser = currentUser;

        public async Task<Result> AddProductAsync(int productId, int quantity)
        {
            var userId = this.currentUser.UserId;

            var shoppingCart = await this.FindByUserId(userId) ?? new ShoppingCart
            {
                UserId = userId
            };

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ShoppingCart = shoppingCart,
                ProductId = productId,
                Quantity = quantity
            };

            await this.Data.AddAsync(shoppingCartProduct);
            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> UpdateProductAsync(int productId, int quantity)
        {
            var userId = this.currentUser.UserId;

            var shoppingCart = await this.FindByProductAndUserAsync(productId, userId);

            if (shoppingCart == null)
            {
                return "This user cannot edit this shopping cart.";
            }

            shoppingCart.Quantity = quantity;

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> RemoveProductAsync(int productId)
        {
            var userId = this.currentUser.UserId;

            var shoppingCart = await this.FindByProductAndUserAsync(productId, userId);

            if (shoppingCart == null)
            {
                return "This user cannot delete products from this shopping cart.";
            }

            this.Data.Remove(shoppingCart);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<int> CountAsync()
            => await this
                .AllByUserId(this.currentUser.UserId)
                .CountAsync();

        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> MineAsync()
            => await this.Mapper
                .ProjectTo<ShoppingCartProductsResponseModel>(this
                    .AllByUserId(this.currentUser.UserId)
                    .AsNoTracking())
                .ToListAsync();

        private async Task<ShoppingCartProduct> FindByProductAndUserAsync(
            int productId,
            string userId)
            => await this
                .AllByUserId(userId)
                .FirstOrDefaultAsync(c => c.ProductId == productId);

        private IQueryable<ShoppingCartProduct> AllByUserId(
            string userId)
            => this
                .All()
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Products);

        private async Task<ShoppingCart> FindByUserId(
            string userId)
            => await this
                .All()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();
    }
}
