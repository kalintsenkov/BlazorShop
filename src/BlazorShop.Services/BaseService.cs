namespace BlazorShop.Services
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;

    public abstract class BaseService<TEntity>
        where TEntity : class
    {
        protected BaseService(ApplicationDbContext data, IMapper mapper)
        {
            this.Data = data;
            this.Mapper = mapper;
        }

        protected ApplicationDbContext Data { get; }

        protected IMapper Mapper { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        protected IQueryable<TEntity> AllAsNoTracking() => this.All().AsNoTracking();
    }
}
