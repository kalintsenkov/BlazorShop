namespace BlazorShop.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models;
    using Models.ShoppingCarts;

    public class ShoppingCartsService : BaseService<ShoppingCart>, IShoppingCartsService
    {
        private const string InvalidErrorMessage = "This user cannot edit this shopping cart.";
        private const string NotEnoughProductsMessage = "There are not enough products in stock.";

        public ShoppingCartsService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<Result> AddAsync(
            ShoppingCartRequestModel model, string userId)
        {
            var productQuantity = await this.GetProductQuantityById(model.ProductId);

            if (productQuantity < model.Quantity)
            {
                return NotEnoughProductsMessage;
            }

            var shoppingCart = await this
                .All()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            shoppingCart ??= new ShoppingCart
            {
                UserId = userId
            };

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ShoppingCart = shoppingCart,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            await this.Data.AddAsync(shoppingCartProduct);
            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> UpdateAsync(
            ShoppingCartRequestModel model, string userId)
        {
            var productQuantity = await this.GetProductQuantityById(model.ProductId);

            if (productQuantity < model.Quantity)
            {
                return NotEnoughProductsMessage;
            }

            var shoppingCartProduct = await this.FindByProductAndUserAsync(
                model.ProductId,
                userId);

            if (shoppingCartProduct == null)
            {
                return InvalidErrorMessage;
            }

            shoppingCartProduct.Quantity = model.Quantity;

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(
            ShoppingCartRequestModel model, string userId)
        {
            var shoppingCartProduct = await this.FindByProductAndUserAsync(
                model.ProductId,
                userId);

            if (shoppingCartProduct == null)
            {
                return InvalidErrorMessage;
            }

            this.Data.Remove(shoppingCartProduct);

            await this.Data.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<int> CountAsync(
            string userId)
            => await this
                .AllByUserId(userId)
                .CountAsync();

        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserAsync(
            string userId)
            => await this.Mapper
                .ProjectTo<ShoppingCartProductsResponseModel>(this
                    .AllByUserId(userId)
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
