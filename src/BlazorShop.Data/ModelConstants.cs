namespace BlazorShop.Data
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public class Identity
        {
            public const int MinPasswordLength = 6;
        }

        public class Address
        {
            public const int MaxCountryLength = 255;
            public const int MaxCityLength = 255;
            public const int MaxStateLength = 255;
            public const int MaxDescriptionLength = 1000;
            public const int MaxPhoneNumberLength = 10;
        }

        public class Product
        {
            public const int MaxDescriptionLength = 1000;
            public const int MaxUrlLength = 2048;
            public const int MinQuantity = 1;
            public const int MaxQuantity = int.MaxValue;
            public const string MinPrice = "1";
            public const string MaxPrice = "100000";
        }
    }
}