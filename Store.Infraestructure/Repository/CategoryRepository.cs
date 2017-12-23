using System.Collections.Generic;
using System.Data.Entity;
using Store.Entities.Tables;
using Store.Repository.IRepository;
using System.Linq;

namespace Store.Infraestructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {

        }

        public List<Category> Search(string filterValue)
        {
            return DbSet.AsNoTracking().Where(r => r.Code.Contains(filterValue) || r.Name.Contains(filterValue)).ToList();
        }
    }
}
