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
    using Models.Products;

    public class CategoriesService : BaseService<Category>, ICategoriesService
    {
        public CategoriesService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(CategoriesRequestModel model)
        {
            var category = new Category { Name = model.Name };

            await this.Data.AddAsync(category);
            await this.Data.SaveChangesAsync();

            return category.Id;
        }

        public async Task<Result> UpdateAsync(int id, CategoriesRequestModel model)
        {
            var category = await this.GetByIdAsync(id);

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
            var category = await this.GetByIdAsync(id);

            if (category == null)
            {
                return false;
            }

            this.Data.Remove(category);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> DetailsAsync(int id)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(c => c.Id == id)
                    .SelectMany(c => c.Products))
                .ToListAsync();

        public async Task<IEnumerable<CategoriesListingResponseModel>> AllAsync()
            => await this.Mapper
                .ProjectTo<CategoriesListingResponseModel>(this
                    .AllAsNoTracking())
                .ToListAsync();

        private async Task<Category> GetByIdAsync(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}
