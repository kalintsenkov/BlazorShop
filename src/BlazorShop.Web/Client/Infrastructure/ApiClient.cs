namespace BlazorShop.Web.Client.Infrastructure
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;

    using Extensions;

    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public ApiClient(
            HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<TResponse> GetJsonAsync<TResponse>(string url)
        {
            await this.SetToken();

            return await this.httpClient.GetFromJsonAsync<TResponse>(url);
        }

        public async Task<HttpResponseMessage> PostJsonAsync<TRequest>(string url, TRequest request)
        {
            await this.SetToken();

            return await this.httpClient.PostAsJsonAsync(url, request);
        }

        public async Task<HttpResponseMessage> PutJsonAsync<TRequest>(string url, TRequest request)
        {
            await this.SetToken();

            return await this.httpClient.PutAsJsonAsync(url, request);
        }

        public async Task<HttpResponseMessage> DeleteJsonAsync<TRequest>(string url, TRequest request)
        {
            await this.SetToken();

            return await this.httpClient.DeleteAsJsonAsync(url, request);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            await this.SetToken();

            return await this.httpClient.DeleteAsync(url);
        }

        private async Task SetToken()
        {
            var token = await this.localStorage.GetItemAsStringAsync("authToken");

            if (token != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
