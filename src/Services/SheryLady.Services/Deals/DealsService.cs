namespace SheryLady.Services.Deals
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using DateTime;

    public class DealsService : IDealsService
    {
        private readonly ApplicationDbContext db;
        private readonly IDateTimeProvider dateTimeProvider;

        public DealsService(
            ApplicationDbContext db, 
            IDateTimeProvider dateTimeProvider)
        {
            this.db = db;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<int> StartAsync(DateTime startDate, DateTime endDate, decimal discount, int productId)
        {
            var deal = new Deal
            {
                StartDate = startDate,
                EndDate = endDate,
                Discount = discount,
                ProductId = productId,
                CreatedOn = this.dateTimeProvider.Now()
            };

            await this.db.Deals.AddAsync(deal);
            await this.db.SaveChangesAsync();

            return deal.Id;
        }

        public async Task<bool> UpdateAsync(int id, DateTime startDate, DateTime endDate, decimal discount)
        {
            var deal = await this.GetByIdAsync(id);
            if (deal == null)
            {
                return false;
            }

            deal.StartDate = startDate;
            deal.EndDate = endDate;
            deal.Discount = discount;
            deal.ModifiedOn = this.dateTimeProvider.Now();

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EndAsync(int id)
        {
            var deal = await this.GetByIdAsync(id);
            if (deal == null)
            {
                return false;
            }

            deal.IsDeleted = true;
            deal.DeletedOn = this.dateTimeProvider.Now();

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<Deal> GetByIdAsync(int id)
            => await this.db
                .Deals
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
    }
}
