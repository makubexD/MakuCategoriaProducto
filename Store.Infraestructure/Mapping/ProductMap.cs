using Store.Entities.Tables;
using System.Data.Entity.ModelConfiguration;

namespace Store.Infraestructure.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable(nameof(Product));
            HasKey(r => r.Code);
            HasRequired(r => r.Category)
                .WithMany(r => r.Products)
                .HasForeignKey(r => r.CategoryCode);
        }
    }
}
