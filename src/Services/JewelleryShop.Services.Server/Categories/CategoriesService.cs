namespace JewelleryShop.Services.Server.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using DateTime;
    using Web.Shared.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public CategoriesService(
            ApplicationDbContext db,
            IMapper mapper, 
            IDateTimeProvider dateTimeProvider)
        {
            this.db = db;
            this.mapper = mapper;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name,
                CreatedOn = this.dateTimeProvider.Now()
            };

            await this.db.Categories.AddAsync(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var category = await this.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Name = name;
            category.ModifiedOn = this.dateTimeProvider.Now();

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await this.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            category.IsDeleted = true;
            category.DeletedOn = this.dateTimeProvider.Now();

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoriesListingResponseModel>> GetAllAsync()
            => await this.db
                .Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ProjectTo<CategoriesListingResponseModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

        private async Task<Category> GetByIdAsync(int id)
            => await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }
}
