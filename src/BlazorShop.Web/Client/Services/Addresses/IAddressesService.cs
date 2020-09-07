namespace BlazorShop.Web.Client.Services.Addresses
{
    using System.Threading.Tasks;

    using Models;
    using Models.Addresses;

    public interface IAddressesService
    {
        Task<int> CreateAsync(AddressesRequestModel model);

        Task<Result> DeleteAsync(int id);

        Task<AddressesListingResponseModel[]> MineAsync();
    }
}
