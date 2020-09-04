namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Identity;
    using Models.Orders;

    public class OrdersService : BaseService<Order>, IOrdersService
    {
        private readonly ICurrentUserService currentUser;

        public OrdersService(
            ApplicationDbContext data,
            IMapper mapper,
            ICurrentUserService currentUser)
            : base(data, mapper)
            => this.currentUser = currentUser;

        public async Task<string> PurchaseAsync(OrdersRequestModel model)
        {
            var userId = this.currentUser.UserId;

            var order = new Order
            {
                UserId = userId,
                DeliveryAddressId = model.AddressId
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

        public async Task<OrdersDetailsResponseModel> DetailsAsync(
            string id)
            => await this.Mapper
                .ProjectTo<OrdersDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.Id == id))
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<OrdersListingResponseModel>> MineAsync()
            => await this.Mapper
                .ProjectTo<OrdersListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.UserId == this.currentUser.UserId)
                    .SelectMany(o => o.Products))
                .ToListAsync();
    }
}