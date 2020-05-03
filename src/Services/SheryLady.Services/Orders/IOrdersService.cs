namespace SheryLady.Services.Orders
{
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task CreateAsync(string userId, int productId, int quantity);

        Task CompleteAsync(int orderId);

        Task<string> GetIdByUserId(string userId);
    }
}
