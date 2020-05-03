namespace SheryLady.Web.Server.Models.Deals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.ModelConstants;

    public class DealsStartRequestModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(typeof(decimal), ProductPriceMinRange, ProductPriceMaxRange)]
        public decimal Discount { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
