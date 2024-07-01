namespace Application.Responce
{
    public class SaleResponse
    {
        public int Id { get; set; }
        public decimal TotalPay { get; set; }
        public int TotalQuantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }
        public List<SaleProductResponse> Products { get; set; }
    }
}
