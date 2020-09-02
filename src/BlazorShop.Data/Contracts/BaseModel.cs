namespace BlazorShop.Data.Contracts
{
    using System;

    public abstract class BaseModel : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}