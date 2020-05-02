namespace SheryLady.Web.Server.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants;

    public class CategoriesCreateRequestModel
    {
        [Required]
        [StringLength(CategoryNameMaxLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; }
    }
}
