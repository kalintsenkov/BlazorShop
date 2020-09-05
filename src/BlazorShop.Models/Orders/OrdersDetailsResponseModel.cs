namespace BlazorShop.Models.Orders
{
    using System.Collections.Generic;

    public class OrdersDetailsResponseModel : OrdersBaseResponseModel
    {
        public IEnumerable<OrdersProductsResponseModel> Products { get; set; }
    }
}
