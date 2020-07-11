namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models.Categories;

    public class CategoriesService : BaseService<Category>, ICategoriesService
    {
        public CategoriesService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category { Name = name };

            await this.Data.AddAsync(category);
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

        public async Task<IEnumerable<CategoryListingResponseModel>> GetAllAsync()
            => await this.Mapper
                .ProjectTo<CategoryListingResponseModel>(this.AllAsNoTracking())
                .ToListAsync();

        private async Task<Category> GetByIdAsync(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}
