namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> wishList)
        {
            wishList
                .HasKey(w => new { w.ProductId, w.UserId });

            wishList
                .HasOne(w => w.Product)
                .WithMany(p => p.WishLists)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            wishList
                .HasOne(w => w.User)
                .WithMany(u => u.WishLists)
                .HasForeignKey(w => w.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            wishList
                .HasIndex(w => w.IsDeleted);
        }
    }
}
