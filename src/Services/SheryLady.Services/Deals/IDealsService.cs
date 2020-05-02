namespace SheryLady.Services.Deals
{
    using System;
    using System.Threading.Tasks;

    public interface IDealsService
    {
        Task<int> Start(DateTime startDate, DateTime endDate, decimal discount, int productId);

        Task<bool> Update(int id, DateTime startDate, DateTime endDate, decimal discount);

        Task<bool> End(int id);
    }
}