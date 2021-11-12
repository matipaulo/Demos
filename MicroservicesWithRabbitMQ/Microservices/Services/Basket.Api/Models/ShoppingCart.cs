namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Id = Guid.NewGuid();
            Items = new List<ShoppingCartItem>();
        }

        public Guid Id { get; }

        public ICollection<ShoppingCartItem> Items { get; set; }

        public decimal Total => Items.Sum(x => x.Price * x.Quantity);
    }

    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}