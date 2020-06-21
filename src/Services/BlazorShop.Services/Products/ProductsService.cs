namespace BlazorShop.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Web.Shared.Products;

    public class ProductsService : BaseService<Product>, IProductsService
    {
        public ProductsService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(
            string name,
            string description,
            string imageSource,
            int quantity,
            decimal price,
            int categoryId)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                ImageSource = imageSource,
                Quantity = quantity,
                Price = price,
                CategoryId = categoryId
            };

            await this.Data.Products.AddAsync(product);
            await this.Data.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> UpdateAsync(
            int id,
            string name,
            string description,
            string imageSource,
            int quantity,
            decimal price,
            int categoryId)
        {
            var product = await this.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            product.Name = name;
            product.Description = description;
            product.ImageSource = imageSource;
            product.Quantity = quantity;
            product.Price = price;
            product.CategoryId = categoryId;

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await this.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            this.Data.Remove(product);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<ProductsDetailsResponseModel> DetailsAsync(int id)
            => await this.Mapper
                .ProjectTo<ProductsDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(p => p.Id == id))
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<ProductsListingResponseModel>> GetAllAsync()
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this.AllAsNoTracking())
                .ToListAsync();

        public async Task<IEnumerable<ProductsListingResponseModel>> GetAllByCategoryIdAsync(int categoryId)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(p => p.CategoryId == categoryId))
                .ToListAsync();

        private async Task<Product> GetByIdAsync(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}
