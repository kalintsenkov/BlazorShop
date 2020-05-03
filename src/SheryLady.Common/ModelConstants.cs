namespace SheryLady.Common
{
    public class ModelConstants
    {
        public const int UserFirstNameMaxLength = 50;
        public const int UserLastNameMaxLength = 50;
        public const int UserUserNameMinLength = 3;
        public const int UserUserNameMaxLength = 50;
        public const int UserPasswordMinLength = 6;
        public const int UserProfilePictureMaxLength = 250;

        public const int ProductNameMinLength = 3;
        public const int ProductNameMaxLength = 30;
        public const int ProductDescriptionMaxLength = 250;
        public const int ProductImageMaxLength = 250;
        public const int ProductQuantityMinRange = 1;
        public const int ProductQuantityMaxRange = int.MaxValue;
        public const string ProductPriceMinRange = "0.01";
        public const string ProductPriceMaxRange = "79228162514264337593543950335";

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 30;
    }
}