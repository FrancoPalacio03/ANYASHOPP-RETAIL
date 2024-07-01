namespace Application.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string imageUrl { get; set; }
        public int Category { get; set; }
    }
}
