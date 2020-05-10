namespace JewelleryShop.Web.Client.Infrastructure.Extensions
{
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    public static class JSRuntimeExtensions
    {
        public static async Task RemoveItem(this IJSRuntime jsRuntime, string item) 
            => await jsRuntime.InvokeAsync<object>("localStorage.removeItem", item);

        public static async Task SetItem(this IJSRuntime jsRuntime, string item, object jsonItem) 
            => await jsRuntime.InvokeAsync<object>("localStorage.setItem", item, jsonItem);

        public static async ValueTask<T> GetItem<T>(this IJSRuntime jsRuntime, T item) 
            => await jsRuntime.InvokeAsync<T>("localStorage.getItem", item);
    }
}
