namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models;
    using Models.Categories;

    public class CategoriesService : BaseService<Category>, ICategoriesService
    {
        public CategoriesService(BlazorShopDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(CategoriesRequestModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };

            await this.Data.AddAsync(category);
            await this.Data.SaveChangesAsync();

            return category.Id;
        }

        public async Task<Result> UpdateAsync(
            int id, CategoriesRequestModel model)
        {
            var category = await this.FindByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            category.Name = model.Name;

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var category = await this.FindByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            this.Data.Remove(category);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoriesListingResponseModel>> AllAsync()
            => await this.Mapper
                .ProjectTo<CategoriesListingResponseModel>(this
                    .AllAsNoTracking())
                .ToListAsync();

        private async Task<Category> FindByIdAsync(
            int id)
            => await this
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
    }
}
