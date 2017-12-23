using System.Collections.Generic;
using System.Data.Entity;
using Store.Entities.Tables;
using Store.Repository.IRepository;
using System.Linq;

namespace Store.Infraestructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public List<Product> Search(string filterValue)
        {
            return DbSet.AsNoTracking().Include(c => c.Category).Where(r => r.Code.Contains(filterValue) || r.Name.Contains(filterValue)).ToList();
        }
    }
}
