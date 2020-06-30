namespace BlazorShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Shared.Models.Orders;

    public class OrdersService : BaseService<Order>, IOrdersService
    {
        public OrdersService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<string> PurchaseAsync(int deliveryAddressId, string userId)
        {
            var order = new Order
            {
                UserId = userId,
                DeliveryAddressId = deliveryAddressId
            };

            await this.Data.AddAsync(order);

            var shoppingCart = await this
                .Data
                .ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .ToListAsync();

            foreach (var cart in shoppingCart)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity
                };

                await this.Data.AddAsync(orderProduct);
            }

            this.Data.RemoveRange(shoppingCart);

            await this.Data.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderDetailsResponseModel> DetailsAsync(string id)
            => await this.Mapper
                .ProjectTo<OrderDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.Id == id))
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<OrderListingResponseModel>> GetAllByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<OrderListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.UserId == userId))
                .ToListAsync();
    }
}