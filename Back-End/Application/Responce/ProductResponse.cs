﻿namespace Application.Responce
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string imageUrl { get; set; }
        public category category { get; set; }
    }
}
