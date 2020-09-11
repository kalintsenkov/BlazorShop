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
        public OrdersService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<string> PurchaseAsync(string userId, OrdersRequestModel model)
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

            foreach (var product in shoppingCartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                };

                await this.Data.AddAsync(orderProduct);
            }

            this.Data.RemoveRange(shoppingCartProducts);

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
    }
}
