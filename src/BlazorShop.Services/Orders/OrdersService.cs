namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models.Orders;

    public class OrdersService : BaseService<Order>, IOrdersService
    {
        public OrdersService(BlazorShopDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<string> PurchaseAsync(
            OrdersRequestModel model, string userId)
        {
            var order = new Order
            {
                UserId = userId,
                DeliveryAddressId = model.AddressId
            };

            var shoppingCartProducts = await this
                .Data
                .ShoppingCartsProducts
                .Where(sc => sc.ShoppingCart.UserId == userId)
                .ToListAsync();

            var orderProducts = new List<OrderProduct>();

            foreach (var shoppingCartProduct in shoppingCartProducts)
            {
                var productId = shoppingCartProduct.ProductId;
                var requestQuantity = shoppingCartProduct.Quantity;

                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductId = productId,
                    Quantity = requestQuantity
                };

                orderProducts.Add(orderProduct);

                await this.ReduceProductQuantity(productId, requestQuantity);
            }

            this.Data.RemoveRange(shoppingCartProducts);

            await this.Data.AddRangeAsync(orderProducts);
            await this.Data.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrdersDetailsResponseModel> DetailsAsync(
            string id)
            => await this.Mapper
                .ProjectTo<OrdersDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.Id == id))
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<OrdersListingResponseModel>> ByUserAsync(
            string userId)
            => await this.Mapper
                .ProjectTo<OrdersListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.UserId == userId)
                    .SelectMany(o => o.Products))
                .ToListAsync();

        private async Task ReduceProductQuantity(
            int productId,
            int requestQuantity)
        {
            var product = await this
                .Data
                .Products
                .FindAsync(productId);

            product.Quantity -= requestQuantity;
        }
    }
}
