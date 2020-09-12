namespace BlazorShop.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        //[Theory]
        //[InlineData(3)]
        //[InlineData(9)]
        //[InlineData(12)]
        //public void AllShouldReturnResultWithCorrectModel(int count)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithData(CategoriesTestData.GetCategories(count)))
        //        .Calling(c => c.All())
        //        .ShouldReturn()
        //        .ResultOfType<IEnumerable<CategoriesListingResponseModel>>(result => result
        //            .Passing(categoriesListing => categoriesListing
        //                .Count()
        //                .ShouldBe(count)));

        //[Fact]
        //public void DetailsShouldReturnEmptyCollectionWhenInvalidCategoryId()
        //    => MyController<CategoriesController>
        //        .Calling(c => c.Details(int.MaxValue))
        //        .ShouldReturn()
        //        .ResultOfType<IEnumerable<ProductsListingResponseModel>>(result => result
        //            .Passing(categoriesListing => categoriesListing
        //                .Count()
        //                .ShouldBe(0)));

        //[Fact]
        //public void CreateShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedAdministrators()
        //    => MyController<CategoriesController>
        //        .Calling(c => c.Create(With.Empty<CategoriesRequestModel>()))
        //        .ShouldHave()
        //        .ActionAttributes(attrs => attrs
        //            .RestrictingForHttpMethod(HttpMethod.Post)
        //            .RestrictingForAuthorizedRequests(Constants.AdministratorRole));

        //[Theory]
        //[InlineData("Category 1")]
        //[InlineData("Category 2")]
        //public void CreateShouldReturnCreatedResultWhenValidModelState(string name)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole }))
        //        .Calling(c => c.Create(new CategoriesRequestModel
        //        {
        //            Name = name
        //        }))
        //        .ShouldHave()
        //        .ValidModelState()
        //        .AndAlso()
        //        .ShouldReturn()
        //        .Created();

        //[Fact]
        //public void CreateShouldHaveInvalidModelStateWhenRequestModelIsInvalid()
        //    => MyController<CategoriesController>
        //        .Calling(c => c.Create(new CategoriesRequestModel()))
        //        .ShouldHave()
        //        .InvalidModelState();

        //[Fact]
        //public void UpdateShouldHaveRestrictionsForHttpPutOnlyAndAuthorizedAdministrators()
        //    => MyController<CategoriesController>
        //        .Calling(c => c.Update(With.Any<int>(), With.Empty<CategoriesRequestModel>()))
        //        .ShouldHave()
        //        .ActionAttributes(attrs => attrs
        //            .RestrictingForHttpMethod(HttpMethod.Put)
        //            .RestrictingForAuthorizedRequests(Constants.AdministratorRole));

        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public void UpdateShouldHaveInvalidModelStateWhenRequestModelIsInvalid(int id)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole })
        //            .WithData(CategoriesTestData.GetCategories(3)))
        //        .Calling(c => c.Update(id, new CategoriesRequestModel()))
        //        .ShouldHave()
        //        .InvalidModelState();

        //[Theory]
        //[InlineData(1, "Updated 1")]
        //[InlineData(2, "Updated 2")]
        //public void UpdateShouldReturnOkResultWhenValidModelState(int id, string updatedName)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole })
        //            .WithData(CategoriesTestData.GetCategories(3)))
        //        .Calling(c => c.Update(id, new CategoriesRequestModel
        //        {
        //            Name = updatedName
        //        }))
        //        .ShouldHave()
        //        .ValidModelState()
        //        .AndAlso()
        //        .ShouldReturn()
        //        .Ok();

        //[Theory]
        //[InlineData("Invalid update 1")]
        //[InlineData("Invalid update 2")]
        //public void UpdateShouldReturnBadRequestWhenCategoryIdIsNotExisting(string updatedName)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole }))
        //        .Calling(c => c.Update(With.Any<int>(), new CategoriesRequestModel
        //        {
        //            Name = updatedName
        //        }))
        //        .ShouldReturn()
        //        .BadRequest();

        //[Fact]
        //public void DeleteShouldHaveRestrictionsForHttpDeleteOnlyAndAuthorizedAdministrators()
        //    => MyController<CategoriesController>
        //        .Calling(c => c.Delete(With.Any<int>()))
        //        .ShouldHave()
        //        .ActionAttributes(attrs => attrs
        //            .RestrictingForHttpMethod(HttpMethod.Delete)
        //            .RestrictingForAuthorizedRequests(Constants.AdministratorRole));

        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public void DeleteShouldReturnOkResultWhenCategoryIsDeleted(int id)
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole })
        //            .WithData(CategoriesTestData.GetCategories(3)))
        //        .Calling(c => c.Delete(id))
        //        .ShouldReturn()
        //        .Ok();

        //[Fact]
        //public void DeleteShouldReturnBadRequestWhenCategoryIdIsNotExisting()
        //    => MyController<CategoriesController>
        //        .Instance(instance => instance
        //            .WithUser(new[] { Constants.AdministratorRole }))
        //        .Calling(c => c.Delete(With.Any<int>()))
        //        .ShouldReturn()
        //        .BadRequest();
    }
}
