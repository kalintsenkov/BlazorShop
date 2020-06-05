namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using  static Common.ModelConstants.Common;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .Property(c => c.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            category
                .HasIndex(c => c.IsDeleted);
        }
    }
}
