namespace SheryLady.Services.Client
{
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    public static class IJSRuntimeExtensions
    {
        public async static Task RemoveItem(this IJSRuntime jsRuntime, string item) 
            => await jsRuntime.InvokeAsync<object>("localStorage.removeItem", item);

        public async static Task SetItem(this IJSRuntime jsRuntime, string item, object jsonItem) 
            => await jsRuntime.InvokeAsync<object>("localStorage.setItem", item, jsonItem);

        public async static ValueTask<T> GetItem<T>(this IJSRuntime jsRuntime, T item) 
            => await jsRuntime.InvokeAsync<T>("localStorage.getItem", item);
    }
}
