namespace BlazorShop.Models.ShoppingCarts
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Product;

    public class ShoppingCartRequestModel
    {
        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; } = MinQuantity;
    }
}