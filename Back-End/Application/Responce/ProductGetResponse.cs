namespace Application.Responce
{
    public class ProductGetResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string imageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
