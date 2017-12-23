using Store.Entities.Tables;
using System.Collections.Generic;

namespace Store.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> Search(string filterValue);
    }
}
