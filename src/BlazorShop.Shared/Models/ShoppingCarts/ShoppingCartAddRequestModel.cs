namespace BlazorShop.Shared.Models.ShoppingCarts
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Product;

    public class ShoppingCartAddRequestModel
    {
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; } = MinQuantity;
    }
}