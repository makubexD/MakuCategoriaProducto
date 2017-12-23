using Store.Entities.Tables;
using System.Collections.Generic;

namespace Store.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> Search(string filterValue);
    }
}
