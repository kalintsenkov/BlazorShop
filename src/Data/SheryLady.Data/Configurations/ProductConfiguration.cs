namespace SheryLady.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.ModelConstants;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product
                .Property(p => p.Name)
                .HasMaxLength(ProductNameMaxLength)
                .IsRequired();

            product
                .Property(p => p.Description)
                .HasMaxLength(ProductDescriptionMaxLength);

            product
                .Property(p => p.Image)
                .IsRequired();

            product
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            product
                .HasIndex(p => p.IsDeleted);
        }
    }
}