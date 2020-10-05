namespace BlazorShop.Web.Client.Infrastructure.Services.Addresses
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Extensions;
    using Models;
    using Models.Addresses;

    public class AddressesService : IAddressesService
    {
        private readonly HttpClient http;

        private const string AddressesPath = "api/addresses";
        private const string AddressesPathWithSlash = AddressesPath + "/";

        public AddressesService(HttpClient http) => this.http = http;

        public async Task<int> CreateAsync(AddressesRequestModel model)
        {
            var addressResponse = await this.http.PostAsJsonAsync(AddressesPath, model);
            var addressId = await addressResponse.Content.ReadFromJsonAsync<int>();

            return addressId;
        }

        public async Task<Result> DeleteAsync(int id)
            => await this.http
                .DeleteAsync(AddressesPathWithSlash + id)
                .ToResult();

        public async Task<IEnumerable<AddressesListingResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<AddressesListingResponseModel>>(AddressesPath);
    }
}
