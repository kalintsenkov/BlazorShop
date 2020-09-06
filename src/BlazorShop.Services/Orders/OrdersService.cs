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

        public async Task<IEnumerable<OrdersListingResponseModel>> MineAsync()
            => await this.Mapper
                .ProjectTo<OrdersListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(o => o.UserId == this.currentUser.UserId)
                    .SelectMany(o => o.Products))
                .ToListAsync();
    }
}