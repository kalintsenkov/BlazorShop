namespace SheryLady.Services.Deals
{
    using System;
    using System.Threading.Tasks;

    public interface IDealsService
    {
        Task<int> StartAsync(DateTime startDate, DateTime endDate, decimal discount, int productId);

        Task<bool> UpdateAsync(int id, DateTime startDate, DateTime endDate, decimal discount);

        Task<bool> EndAsync(int id);
    }
}