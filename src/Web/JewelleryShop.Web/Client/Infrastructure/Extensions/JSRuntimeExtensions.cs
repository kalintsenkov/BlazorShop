namespace JewelleryShop.Web.Client.Infrastructure.Extensions
{
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    public static class JSRuntimeExtensions
    {
        public static async Task RemoveAsync(this IJSRuntime jsRuntime, string item) 
            => await jsRuntime.InvokeAsync<object>("localStorage.removeItem", item);

        public static async Task SetAsync(this IJSRuntime jsRuntime, string item, object value) 
            => await jsRuntime.InvokeAsync<object>("localStorage.setItem", item, value);

        public static async ValueTask<T> GetAsync<T>(this IJSRuntime jsRuntime, T item) 
            => await jsRuntime.InvokeAsync<T>("localStorage.getItem", item);
    }
}
