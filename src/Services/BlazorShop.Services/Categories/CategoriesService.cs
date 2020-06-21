namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CategoriesService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category { Name = name };

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

            this.db.Remove(category);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoriesListingResponseModel>> GetAllAsync()
            => await this.mapper
                .ProjectTo<CategoriesListingResponseModel>(this
                    .All()
                    .AsNoTracking())
                .ToListAsync();

        private async Task<Category> GetByIdAsync(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

        private IQueryable<Category> All()
            => this.db.Categories;
    }
}
