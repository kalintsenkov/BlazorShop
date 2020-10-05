namespace BlazorShop.Web.Client.Infrastructure.Services.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Addresses;

    public interface IAddressesService
    {
        Task<int> CreateAsync(AddressesRequestModel model);

        Task<Result> DeleteAsync(int id);

        Task<IEnumerable<AddressesListingResponseModel>> Mine();
    }
}
