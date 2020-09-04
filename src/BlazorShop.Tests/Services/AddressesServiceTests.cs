namespace BlazorShop.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using BlazorShop.Data.Models;
    using BlazorShop.Services.Addresses;
    using Data;
    using Models.Addresses;

    public class AddressesServiceTests : SetupFixture
    {
        private readonly IAddressesService addresses;

        public AddressesServiceTests() 
            => this.addresses = new AddressesService(
                this.Data, 
                this.Mapper,
                this.CurrentUser);

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
            var model = new AddressesRequestModel
            {
                Country = country,
                State = state,
                City = city,
                Description = description,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber
            };

            var id = await this.addresses.CreateAsync(model);

            var expected = new Address
            {
                Id = id,
                Country = country,
                State = state,
                City = city,
                Description = description,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber,
                UserId = TestUser.Identifier
            };

            var actual = await this
                .Data
                .Addresses
                .FirstAsync();

            this.Data.Addresses.Count().ShouldBe(1);

            actual.Id.ShouldBe(expected.Id);
            actual.Country.ShouldBe(expected.Country);
            actual.State.ShouldBe(expected.State);
            actual.Description.ShouldBe(expected.Description);
            actual.PostalCode.ShouldBe(expected.PostalCode);
            actual.PhoneNumber.ShouldBe(expected.PhoneNumber);
            actual.UserId.ShouldBe(expected.UserId);
        }

        [Theory]
        [InlineData("Country 1", "State 1", "City 1", "Test description 1", "1000", "0888888888")]
        [InlineData("Country 2", "State 2", "City 2", "Test description 2", "2000", "0888888888")]
        [InlineData("Country 3", "State 3", "City 3", "Test description 3", "3000", "0888888888")]
        public async Task DeleteShouldDeleteAddressFromDatabase(
            string country,
            string state,
            string city,
            string description,
            string postalCode,
            string phoneNumber)
        {
            var model = new AddressesRequestModel
            {
                Country = country,
                State = state,
                City = city,
                Description = description,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber
            };

            var id = await this.addresses.CreateAsync(model);

            await this.addresses.DeleteAsync(id);

            this.Data.Addresses.Count().ShouldBe(0);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        public async Task MineShouldReturnCurrentUserAddresses(int count)
        {
            var data = AddressesTestData.GetAddresses(count);

            await this.Data.Addresses.AddRangeAsync(data);

            await this.Data.SaveChangesAsync();

            var actual = await this.addresses.MineAsync();

            actual.Count().ShouldBe(count);
            actual.ShouldBeOfType<List<AddressesListingResponseModel>>();
        }
    }
}
