using System.Collections.Generic;

namespace Store.Entities.Tables
{
    public class Category
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
