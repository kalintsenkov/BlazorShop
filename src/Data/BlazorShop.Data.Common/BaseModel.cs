namespace BlazorShop.Data.Interfaces
{
    using System;

    public abstract class BaseModel<TKey> : IAuditInfo
        where TKey : struct
    {
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}