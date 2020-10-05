namespace BlazorShop.Web.Client.Infrastructure.Extensions
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models;

    public static class ResultExtensions
    {
        public static async Task<Result> ToResult(this Task<HttpResponseMessage> responseTask)
        {
            var response = await responseTask;

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            return Result.Success;
        }
    }
}
