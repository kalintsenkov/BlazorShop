namespace SheryLady.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Data.Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CategoriesService(
            ApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<int> Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await this.db.Categories.AddAsync(category);
            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> Update(int id, string name)
        {
            var category = await this.GetById(id);
            if (category == null)
            {
                return false;
            }

            category.Name = name;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await this.GetById(id);
            if (category == null)
            {
                return false;
            }

            this.db.Remove(category);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<TModel> GetById<TModel>(int id)
            => await this.db
                .Categories
                .Where(c => c.Id == id && !c.IsDeleted)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        private async Task<Category> GetById(int id)
            => await this.db
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }
}
