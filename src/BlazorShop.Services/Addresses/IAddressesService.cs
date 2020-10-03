namespace BlazorShop.Services.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.Addresses;

    public interface IAddressesService : IService
    {
        Task<int> CreateAsync(AddressesRequestModel model, string userId);

        Task<Result> DeleteAsync(int id, string userId);

        Task<IEnumerable<AddressesListingResponseModel>> ByUserAsync(string userId);
    }
}