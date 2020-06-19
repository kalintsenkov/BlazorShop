namespace BlazorShop.Web.Shared.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Common;
    using static ErrorMessages;

    public class CategoriesCreateRequestModel
    {
        [Required]
        [StringLength(MaxNameLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = MinNameLength)]
        public string Name { get; set; }
    }
}
