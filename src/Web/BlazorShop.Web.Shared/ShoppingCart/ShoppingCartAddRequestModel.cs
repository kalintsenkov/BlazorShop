namespace BlazorShop.Web.Shared.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Product;

    public class ShoppingCartAddRequestModel
    {
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; } = DefaultQuantity;
    }
}