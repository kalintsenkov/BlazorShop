namespace BlazorShop.Services.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Addresses;

    public interface IAddressesService
    {
        Task<int> CreateAsync(AddressesRequestModel model, string userId);

        Task<Result> DeleteAsync(int id, string userId);

        Task<IEnumerable<AddressesListingResponseModel>> ByUserAsync(string userId);
    }
}