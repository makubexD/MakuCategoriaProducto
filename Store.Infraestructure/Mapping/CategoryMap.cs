using Store.Entities.Tables;
using System.Data.Entity.ModelConfiguration;

namespace Store.Infraestructure.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable(nameof(Category));
            HasKey(r => r.Code);
        }
    }
}
