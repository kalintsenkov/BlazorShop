namespace BlazorShop.Data.Contracts
{
    using System;

    public abstract class BaseModel<TKey> : IAuditInfo
    {
        public virtual TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}