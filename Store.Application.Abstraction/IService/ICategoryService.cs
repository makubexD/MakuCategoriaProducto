using System.Collections.Generic;
using Store.DTO;
using Store.DTO.Common;

namespace Store.Application.Abstraction.IService
{
    public interface ICategoryService
    {
        OperationResult<List<CategoryDto>> Search(string filterValue);
    }
}
