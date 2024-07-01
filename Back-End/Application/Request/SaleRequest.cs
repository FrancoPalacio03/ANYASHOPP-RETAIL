namespace Application.Request
{
    public class SaleRequest
    {
        public List<SaleProductRequest> Products { get; set; }
        public decimal TotalPayed { get; set; }
    }
}
