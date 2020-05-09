namespace JewelleryShop.Services.Server.DateTime
{
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
