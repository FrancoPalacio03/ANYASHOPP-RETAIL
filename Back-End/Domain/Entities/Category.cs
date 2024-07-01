namespace Domain.Entities
{
    public class Category
    {
        public int CategoryId { set; get; }
        public string Name { set; get; }

        public ICollection<Product> products { get; set; }
    }
}
