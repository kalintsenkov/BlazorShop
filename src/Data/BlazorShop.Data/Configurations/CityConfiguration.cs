namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static Common.ModelConstants.Region;

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> city)
        {
            city
                .Property(c => c.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            city
                .HasOne(c => c.Region)
                .WithMany(r => r.Cities)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
