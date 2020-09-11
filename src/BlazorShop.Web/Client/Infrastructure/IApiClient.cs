namespace BlazorShop.Web.Client.Infrastructure
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IApiClient
    {
        Task<TResponse> GetJsonAsync<TResponse>(string url);

        Task<HttpResponseMessage> PostJsonAsync<TRequest>(string url, TRequest request);

        Task<HttpResponseMessage> PutJsonAsync<TRequest>(string url, TRequest request);

        Task<HttpResponseMessage> DeleteJsonAsync<TRequest>(string url, TRequest request);

        Task<HttpResponseMessage> DeleteAsync(string url);
    }
}
