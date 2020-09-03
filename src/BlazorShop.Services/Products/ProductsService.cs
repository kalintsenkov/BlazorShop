namespace BlazorShop.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models;
    using Models.Products;
    using Specifications;
    using Specifications.Products;

    public class ProductsService : BaseService<Product>, IProductsService
    {
        public ProductsService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task<int> CreateAsync(ProductsRequestModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageSource = model.ImageSource,
                Quantity = model.Quantity,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            await this.Data.AddAsync(product);
            await this.Data.SaveChangesAsync();

            return product.Id;
        }

        public async Task<Result> UpdateAsync(int id, ProductsRequestModel model)
        {
            var product = await this.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.ImageSource = model.ImageSource;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeleteAsync(int id)
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

        public async Task<ProductsDetailsResponseModel> DetailsAsync(
            int id)
            => await this.Mapper
                .ProjectTo<ProductsDetailsResponseModel>(this
                    .AllAsNoTracking()
                    .Where(this.GetProductSpecification(id)))
                .FirstOrDefaultAsync();

        private async Task<Product> GetByIdAsync(
            int id)
            => await this
                .All()
                .FirstOrDefaultAsync(this.GetProductSpecification(id));

        private Specification<Product> GetProductSpecification(
            int id)
            => new ProductByIdSpecification(id);
    }
}
