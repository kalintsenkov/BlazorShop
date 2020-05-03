namespace SheryLady.Web.Server.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants;

    public class ProductsCreateRequestModel
    {
        [Required]
        [StringLength(ProductNameMaxLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = ProductNameMinLength)]
        public string Name { get; set; }

        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ProductImageMaxLength)]
        public string Image { get; set; }
        
        [Required]
        [Range(ProductQuantityMinRange, ProductQuantityMaxRange)]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), ProductPriceMinRange, ProductPriceMaxRange)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
