namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static ModelConstants.Common;

    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .Property(c => c.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            category
                .HasIndex(c => c.IsDeleted);

            category
                .HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
