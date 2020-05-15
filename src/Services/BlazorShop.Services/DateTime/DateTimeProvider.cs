namespace BlazorShop.Services.DateTime
{
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
