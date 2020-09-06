namespace BlazorShop.Web.Client.Infrastructure
{
    using System.Threading.Tasks;

    using Models;

    public interface IApiClient
    {
        Task<TResponse> GetJson<TResponse>(string url);

        Task<TResponse> PostJson<TRequest, TResponse>(string url, TRequest request);

        Task<Result> PostJson<TRequest>(string url, TRequest request);

        Task<Result> PutJson<TRequest>(string url, TRequest request);

        Task<Result> Delete(string url);
    }
}
