namespace BlazorShop.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using BlazorShop.Services.Addresses;
    using Common;
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
        public async Task CreateShouldWorkCorrectly(
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

            address.Id.ShouldBe(id);
            address.Country.ShouldBe(request.Country);
            address.State.ShouldBe(request.State);
            address.Description.ShouldBe(request.Description);
            address.PostalCode.ShouldBe(request.PostalCode);
            address.PhoneNumber.ShouldBe(request.PhoneNumber);
            address.UserId.ShouldBe(userId);
        }

        [Fact]
        public async Task DeleteShouldReturnSucceededResultWhenAddressIsDeleted()
        {
            await this.AddFakeAddresses(1);

            var result = await this.addresses.DeleteAsync(1, TestUser.Identifier);

            result.Succeeded.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteShouldSetIsDeletedToTrue()
        {
            await this.AddFakeAddresses(1);

            var result = await this.addresses.DeleteAsync(1, TestUser.Identifier);

            var address = await this
                .Data
                .Addresses
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();

            result.Succeeded.ShouldBeTrue();
            address.ShouldNotBeNull();
            address.IsDeleted.ShouldBeTrue();
        }

        [Fact]
        public async Task DeleteShouldReturnNotSucceededResultWhenAddressIsNotFound()
        {
            var result = await this.addresses.DeleteAsync(1, TestUser.Identifier);

            result.Succeeded.ShouldBeFalse();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        public async Task ByUserShouldReturnCurrentUserAddresses(int count)
        {
            await this.AddFakeAddresses(count);

            var actual = await this.addresses.ByUserAsync(TestUser.Identifier);

            actual.Count().ShouldBe(count);
            actual.ShouldBeAssignableTo<IEnumerable<AddressesListingResponseModel>>();
        }

        private async Task AddFakeAddresses(int count)
        {
            var fakes = AddressesTestData.GetAddresses(count);

            await this.Data.AddRangeAsync(fakes);
            await this.Data.SaveChangesAsync();
        }
    }
}
