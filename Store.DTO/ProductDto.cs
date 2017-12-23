using System.ComponentModel.DataAnnotations;

namespace Store.DTO
{
    public class ProductDto
    {
        [Required(ErrorMessage = "El código es requerido")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }

        [Required(ErrorMessage = "El código de categoria es requerido")]
        public string CategoryCode { get; set; }
        public bool IsXml { get; set; }
    }
}
