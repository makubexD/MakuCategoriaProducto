using System;

namespace Store.DTO.ProductXml
{
    [Serializable]
    public class ProducXmlDto
    {
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Stock { get; set; }
        
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}
