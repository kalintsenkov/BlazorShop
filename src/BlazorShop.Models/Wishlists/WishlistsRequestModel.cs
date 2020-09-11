namespace BlazorShop.Models.Wishlists
{
    using System.ComponentModel.DataAnnotations;

    public class WishlistsRequestModel
    {
        [Required]
        public int ProductId { get; set; }
    }
}
