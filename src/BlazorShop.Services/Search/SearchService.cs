namespace BlazorShop.Services.Search
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models.Products;
    using Models.Search;
    using Specifications;
    using Specifications.Products;

    using static Common.Constants;

    public class SearchService : BaseService<Product>, ISearchService
    {
        public SearchService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> Products(
            SearchRequestModel model)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(this.GetProductSpecification(model))
                    .Skip((model.Page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage))
                .ToListAsync();

        private Specification<Product> GetProductSpecification(
            SearchRequestModel model)
            => new ProductByNameSpecification(model.Query)
                .And(new ProductByPriceSpecification(model.MinPrice, model.MaxPrice))
                .And(new ProductByCategorySpecification(model.Category));
    }
}