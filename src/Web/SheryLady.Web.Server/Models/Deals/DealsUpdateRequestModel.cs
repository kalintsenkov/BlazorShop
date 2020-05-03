namespace SheryLady.Web.Server.Models.Deals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.ModelConstants;

    public class DealsUpdateRequestModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(typeof(decimal), ProductPriceMinRange, ProductPriceMaxRange)]
        public decimal Discount { get; set; }
    }
}
