namespace BlazorShop.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using Data;
    using Shared.Models.Addresses;
    using Web.Server.Controllers;

    public class AddressesControllerTests
    {
        [Fact]
        public void ShouldHaveRestrictionsForAuthorizedUsers()
            => MyController<AddressesController>
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(12)]
        public void AllShouldReturnResultWithCorrectModel(int count)
            => MyController<AddressesController>
                .Instance(instance => instance
                    .WithUser(TestUser.AuthenticationType)
                    .WithData(AddressTestData.GetAddresses(count)))
                .Calling(c => c.All())
                .ShouldReturn()
                .ResultOfType<IEnumerable<AddressListingResponseModel>>(result => result
                    .Passing(addressListing => addressListing
                        .Count()
                        .ShouldBe(count)));

        [Fact]
        public void CreateShouldHaveRestrictionsForHttpPostOnly()
            => MyController<AddressesController>
                .Calling(c => c.Create(With.Empty<AddressRequestModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post));
    }
}
