namespace SheryLady.Services.Orders
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using DateTime;

    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;
        private readonly IDateTimeProvider dateTimeProvider;

        public OrdersService(
            ApplicationDbContext db, 
            IDateTimeProvider dateTimeProvider)
        {
            this.db = db;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task CreateAsync(string userId, int productId, int quantity)
        {
            var order = new Order
            {
                UserId = userId,
                CreatedOn = this.dateTimeProvider.Now()
            };

            await this.db.Orders.AddAsync(order);

            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = productId,
                Quantity = quantity,
                Status = Status.InProcess,
                CreatedOn = this.dateTimeProvider.Now()
            };

            await this.db.OrdersProducts.AddAsync(orderProduct);
            await this.db.SaveChangesAsync();
        }

        public async Task CompleteAsync(int orderId)
        {
            var orderProducts = await this.db
                .OrdersProducts
                .Where(op => op.OrderId == orderId)
                .ToListAsync();

            foreach (var orderProduct in orderProducts)
            {
                orderProduct.Status = Status.Completed;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task<string> GetIdByUserId(string userId)
            => await this.db
                .Orders
                .Where(o => o.UserId == userId)
                .Select(o => o.UserId)
                .FirstOrDefaultAsync();
    }
}