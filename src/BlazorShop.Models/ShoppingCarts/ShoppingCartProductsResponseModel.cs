namespace BlazorShop.Models.ShoppingCarts
{
    using AutoMapper;

    using Data.Models;

    public class ShoppingCartProductsResponseModel : IMapFrom<ShoppingCart>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<ShoppingCart, ShoppingCartProductsResponseModel>()
                .ForMember(m => m.Id, m => m.MapFrom(c => c.Product.Id))
                .ForMember(m => m.Name, m => m.MapFrom(c => c.Product.Name))
                .ForMember(m => m.Price, m => m.MapFrom(c => c.Product.Price))
                .ForMember(m => m.ImageSource, m => m.MapFrom(c => c.Product.ImageSource))
                .ForMember(m => m.Quantity, m => m.MapFrom(c => c.Quantity));
    }
}