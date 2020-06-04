namespace BlazorShop.Data.Interfaces
{
    using System;

    public class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
        where TKey : struct
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}