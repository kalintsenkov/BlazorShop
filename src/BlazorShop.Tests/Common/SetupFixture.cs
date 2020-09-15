namespace BlazorShop.Tests.Common
{
    using System;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using BlazorShop.Data;
    using Web.Server.Infrastructure;

    public abstract class SetupFixture : IDisposable
    {
        protected SetupFixture()
        {
            this.Data = InitializeDbContext();
            this.Mapper = InitializeAutoMapper();
        }

        protected ApplicationDbContext Data { get; }

        protected IMapper Mapper { get; }

        public void Dispose() => this.Data?.Dispose();

        private static ApplicationDbContext InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private static IMapper InitializeAutoMapper()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            return new Mapper(configuration);
        }
    }
}
