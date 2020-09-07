namespace BlazorShop.Web.Client.Services.Addresses
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models;
    using Models.Addresses;

    public class AddressesService : IAddressesService
    {
        private const string AddressesPath = "api/addresses";

        private readonly IApiClient apiClient;

        public AddressesService(IApiClient apiClient) 
            => this.apiClient = apiClient;

        public async Task<int> CreateAsync(AddressesRequestModel model)
            => await this.apiClient.PostJson<AddressesRequestModel, int>(AddressesPath, model);

        public async Task<Result> DeleteAsync(int id)
            => await this.apiClient.Delete($"{AddressesPath}/{id}");

        public async Task<AddressesListingResponseModel[]> MineAsync()
            => await this.apiClient.GetJson<AddressesListingResponseModel[]>(AddressesPath);
    }
}
