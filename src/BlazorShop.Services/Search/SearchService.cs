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

    using static Common.Constants;

    public class SearchService : BaseService<Product>, ISearchService
    {
        public SearchService(ApplicationDbContext data, IMapper mapper)
            : base(data, mapper)
        {
        }

        public async Task<IEnumerable<ProductsListingResponseModel>> Products(
            string query,
            int page = 1)
            => await this.Mapper
                .ProjectTo<ProductsListingResponseModel>(this
                    .AllAsNoTracking()
                    .Where(p => p.Name.ToLower().Contains(query.ToLower()))
                    .Skip((page - 1) * ProductsPerPage)
                    .Take(ProductsPerPage))
                .ToListAsync();
    }
}