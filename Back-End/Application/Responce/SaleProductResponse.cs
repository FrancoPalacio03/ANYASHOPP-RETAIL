namespace Application.Responce
{
    public class SaleProductResponse
    {
        public int Id { get; set; }
        public string productId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
