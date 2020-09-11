namespace BlazorShop.Services.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Addresses;

    public interface IAddressesService
    {
        Task<int> CreateAsync(string userId, AddressesRequestModel model);

        Task<Result> DeleteAsync(int id, string userId);

        Task<IEnumerable<AddressesListingResponseModel>> ByUserAsync(string userId);
    }
}