namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static ModelConstants.Address;

    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> address)
        {
            address
                .Property(a => a.Country)
                .HasMaxLength(MaxCountryLength)
                .IsRequired();

            address
                .Property(a => a.State)
                .HasMaxLength(MaxStateLength)
                .IsRequired();

            address
                .Property(a => a.City)
                .HasMaxLength(MaxCityLength)
                .IsRequired();

            address
                .Property(a => a.Description)
                .HasMaxLength(MaxDescriptionLength)
                .IsRequired();

            address
                .Property(a => a.PostalCode)
                .HasMaxLength(MaxPostalCodeLength)
                .IsRequired();

            address
                .Property(a => a.PhoneNumber)
                .HasMaxLength(MaxPhoneNumberLength)
                .IsRequired();

            address
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            address
                .HasIndex(a => a.IsDeleted);

            address
                .HasQueryFilter(a => !a.IsDeleted);
        }
    }
}