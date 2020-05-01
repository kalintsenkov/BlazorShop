namespace SheryLady.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> deal)
        {
            deal
                .HasOne(d => d.Product)
                .WithMany(p => p.Deals)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            deal
                .HasIndex(d => d.IsDeleted);
        }
    }
}
