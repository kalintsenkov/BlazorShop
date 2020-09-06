namespace BlazorShop.Web.Client.Infrastructure
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;

    using Models;

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

        public async Task<TResponse> GetJson<TResponse>(string url)
        {
            await this.SetToken();

            return await this.httpClient.GetFromJsonAsync<TResponse>(url);
        }

        public async Task<TResponse> PostJson<TRequest, TResponse>(string url, TRequest request)
        {
            await this.SetToken();

            var response = await this.httpClient.PostAsJsonAsync(url, request);
            var responseObject = await response.Content.ReadFromJsonAsync<TResponse>();

            return responseObject;
        }

        public async Task<Result> PostJson<TRequest>(string url, TRequest request)
        {
            await this.SetToken();

            var response = await this.httpClient.PostAsJsonAsync(url, request);

            return await this.GetResult(response);
        }

        public async Task<Result> PutJson<TRequest>(string url, TRequest request)
        {
            await this.SetToken();

            var response = await this.httpClient.PutAsJsonAsync(url, request);

            return await this.GetResult(response);
        }

        public async Task<Result> Delete(string url)
        {
            await this.SetToken();

            var response = await this.httpClient.DeleteAsync(url);

            return await this.GetResult(response);
        }

        private async Task<Result> GetResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return Result.Success;
            }

            var errors = await response.Content.ReadFromJsonAsync<string[]>();

            return Result.Failure(errors);
        }

        private async Task SetToken()
        {
            var token = await this.localStorage.GetItemAsync<string>("authToken");

            if (token != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
