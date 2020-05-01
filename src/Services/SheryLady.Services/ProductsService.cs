namespace SheryLady.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Data.Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductsService(
            ApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<int> Create(
            string name,
            string description,
            string image,
            int quantity,
            decimal price,
            int categoryId)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                Image = image,
                Quantity = quantity,
                Price = price,
                CategoryId = categoryId
            };

            await this.db.Products.AddAsync(product);
            await this.db.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> Update(
            int id,
            string name,
            string description,
            string image,
            int quantity,
            decimal price,
            int categoryId)
        {
            var product = await this.GetById(id);
            if (product == null)
            {
                return false;
            }

            product.Name = name;
            product.Description = description;
            product.Image = image;
            product.Quantity = quantity;
            product.Price = price;
            product.CategoryId = categoryId;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await this.GetById(id);
            if (product == null)
            {
                return false;
            }

            this.db.Remove(product);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<TModel> GetById<TModel>(int id)
            => await this.db
                .Products
                .Where(p => p.Id == id && !p.IsDeleted)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        private async Task<Product> GetById(int id)
            => await this.db.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}
