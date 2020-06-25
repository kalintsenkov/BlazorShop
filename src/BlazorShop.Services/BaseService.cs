namespace BlazorShop.Services
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;

    public abstract class BaseService<TModel>
        where TModel : class
    {
        protected BaseService(ApplicationDbContext data, IMapper mapper)
        {
            this.Data = data;
            this.Mapper = mapper;
        }

        protected ApplicationDbContext Data { get; }

        protected IMapper Mapper { get; }

        protected IQueryable<TModel> All() => this.Data.Set<TModel>();

        protected IQueryable<TModel> AllAsNoTracking() => this.All().AsNoTracking();
    }
}
