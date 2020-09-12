namespace BlazorShop.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using BlazorShop.Services.Addresses;
    using Data;
    using Models.Addresses;

    public class AddressesServiceTests : SetupFixture
    {
        private readonly IAddressesService addresses;

        public AddressesServiceTests()
            => this.addresses = new AddressesService(this.Data, this.Mapper);

        [Theory]
        [InlineData("Country 1", "State 1", "City 1", "Test description 1", "1000", "0888888888")]
        [InlineData("Country 2", "State 2", "City 2", "Test description 2", "2000", "0888888888")]
        [InlineData("Country 3", "State 3", "City 3", "Test description 3", "3000", "0888888888")]
        public async Task CreateShouldAddRightAddressInDatabase(
            string country,
            string state,
            string city,
            string description,
            string postalCode,
            string phoneNumber)
        {
            const string userId = TestUser.Identifier;

            var request = new AddressesRequestModel
            {
                Country = country,
                State = state,
                City = city,
                Description = description,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber
            };

            var id = await this.addresses.CreateAsync(request, userId);

            var address = await this.Data.Addresses.FindAsync(id);

            this.Data.Addresses.Count().ShouldBe(1);

            address.Id.ShouldBe(id);
            address.Country.ShouldBe(request.Country);
            address.State.ShouldBe(request.State);
            address.Description.ShouldBe(request.Description);
            address.PostalCode.ShouldBe(request.PostalCode);
            address.PhoneNumber.ShouldBe(request.PhoneNumber);
            address.UserId.ShouldBe(userId);
        }

        [Fact]
        public async Task DeleteShouldDeleteAddressFromDatabase()
        {
            await this.SeedAddresses(1);

            var result = await this.addresses.DeleteAsync(1, TestUser.Identifier);

            this.Data.Addresses.Count().ShouldBe(0);

            result.Succeeded.ShouldBeTrue();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        public async Task ByUserShouldReturnCurrentUserAddresses(int count)
        {
            await this.SeedAddresses(count);

            var actual = await this.addresses.ByUserAsync(TestUser.Identifier);

            actual.Count().ShouldBe(count);
            actual.ShouldBeAssignableTo<IEnumerable<AddressesListingResponseModel>>();
        }

        private async Task SeedAddresses(int count)
        {
            var data = AddressesTestData.GetAddresses(count);

            await this.Data.Addresses.AddRangeAsync(data);
            await this.Data.SaveChangesAsync();
        }
    }
}
