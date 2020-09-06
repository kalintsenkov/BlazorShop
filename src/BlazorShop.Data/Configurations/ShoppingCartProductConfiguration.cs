namespace BlazorShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class ShoppingCartProductConfiguration : IEntityTypeConfiguration<ShoppingCartProduct>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProduct> shoppingCart)
        {
            shoppingCart
                .HasKey(sc => new { sc.ShoppingCartId, sc.ProductId });

            shoppingCart
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(u => u.Products)
                .HasForeignKey(sc => sc.ShoppingCartId);

            shoppingCart
                .HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(sc => sc.ProductId);
        }
    }
}
