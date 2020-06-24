namespace BlazorShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Categories;

    public class CategoriesService : BaseService<Category>, ICategoriesService
    {
        public CategoriesService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category { Name = name };

            await this.Data.Categories.AddAsync(category);
            await this.Data.SaveChangesAsync();

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

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await this.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            this.Data.Remove(category);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoriesListingResponseModel>> GetAllAsync()
            => await this.Mapper
                .ProjectTo<CategoriesListingResponseModel>(this.AllAsNoTracking())
                .ToListAsync();

        private async Task<Category> GetByIdAsync(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}
