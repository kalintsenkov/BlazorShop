namespace SheryLady.Web.Server.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants;

    public class CategoriesUpdateRequestModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = CategoryNameMinLength)]

        public string Name { get; set; }
    }
}
