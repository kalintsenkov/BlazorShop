namespace JewelleryShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using  static Common.ModelConstants;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .Property(c => c.Name)
                .HasMaxLength(CategoryNameMaxLength)
                .IsRequired();

            category
                .HasIndex(c => c.IsDeleted);
        }
    }
}
