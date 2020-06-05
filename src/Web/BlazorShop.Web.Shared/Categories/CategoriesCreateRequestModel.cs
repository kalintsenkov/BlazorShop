namespace BlazorShop.Web.Shared.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants.Common;

    public class CategoriesCreateRequestModel
    {
        [Required]
        [StringLength(MaxNameLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = MinNameLength)]
        public string Name { get; set; }
    }
}
