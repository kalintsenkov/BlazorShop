namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class WishlistProductConfiguration : IEntityTypeConfiguration<WishlistProduct>
    {
        public void Configure(EntityTypeBuilder<WishlistProduct> wishlistProduct)
        {
            wishlistProduct
                .HasKey(wp => new { wp.WishlistId, wp.ProductId });

            wishlistProduct
                .HasOne(wp => wp.Wishlist)
                .WithMany(w => w.Products)
                .HasForeignKey(wp => wp.WishlistId);

            wishlistProduct
                .HasOne(wp => wp.Product)
                .WithMany(p => p.Wishlists)
                .HasForeignKey(wp => wp.ProductId);
        }
    }
}
