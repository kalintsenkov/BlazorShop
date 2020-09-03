namespace BlazorShop.Tests
{
    using System;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using BlazorShop.Data;
    using BlazorShop.Services.Identity;
    using Mocks;
    using Web.Server.Infrastructure;

    public abstract class SetupFixture : IDisposable
    {
        protected SetupFixture()
        {
            this.Data = InitializeDbContext();
            this.Mapper = InitializeAutoMapper();
            this.CurrentUser = CurrentUserMock.Create;
        }

        protected ApplicationDbContext Data { get; }

        protected IMapper Mapper { get; }

        protected ICurrentUserService CurrentUser { get; }

        public void Dispose() => this.Data?.Dispose();

        private static ApplicationDbContext InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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
