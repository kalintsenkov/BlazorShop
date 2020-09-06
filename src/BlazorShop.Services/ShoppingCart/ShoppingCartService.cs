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
            var productQuantity = await this.GetProductQuantityById(productId);

            if (productQuantity < quantity)
            {
                return "There are not enough products in stock.";
            }

            var userId = this.currentUser.UserId;

            var shoppingCart = await this.FindByUserId(userId);

            shoppingCart ??= new ShoppingCart
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
            var productQuantity = await this.GetProductQuantityById(productId);

            if (productQuantity < quantity)
            {
                return "There are not enough products in stock.";
            }

            var userId = this.currentUser.UserId;

            var shoppingCartProduct = await this.FindByProductAndUserAsync(productId, userId);

            if (shoppingCartProduct == null)
            {
                return "This user cannot edit this shopping cart.";
            }

            shoppingCartProduct.Quantity = quantity;

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> RemoveProductAsync(int productId)
        {
            var userId = this.currentUser.UserId;

            var shoppingCartProduct = await this.FindByProductAndUserAsync(productId, userId);

            if (shoppingCartProduct == null)
            {
                return "This user cannot delete products from this shopping cart.";
            }

            this.Data.Remove(shoppingCartProduct);

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

        private async Task<ShoppingCart> FindByUserId(
            string userId)
            => await this
                .All()
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

        private IQueryable<ShoppingCartProduct> AllByUserId(
            string userId)
            => this
                .All()
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.Products);

        private async Task<int> GetProductQuantityById(
            int productId)
            => await this
                .Data
                .Products
                .Where(p => p.Id == productId)
                .Select(p => p.Quantity)
                .FirstOrDefaultAsync();
    }
}
