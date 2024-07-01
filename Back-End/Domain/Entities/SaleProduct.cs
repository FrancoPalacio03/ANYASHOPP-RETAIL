namespace Domain.Entities
{
    public class SaleProduct
    {
        public int ShoppingCartId { get; set; }
        public int Sale { get; set; }
        public Guid Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }

        public Product product { get; set; }
        public Sale sale { get; set; }
    }
}
