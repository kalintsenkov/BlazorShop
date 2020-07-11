namespace BlazorShop.Models.Identity
{
    using System.Collections.Generic;

    public class RegisterResponseModel
    {
        public bool Successful { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
