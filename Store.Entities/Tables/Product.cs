namespace Store.Entities.Tables
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public string CategoryCode { get; set; }
        public Category Category { get; set; }
    }
}
