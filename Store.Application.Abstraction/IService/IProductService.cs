using System.Collections.Generic;
using Store.Application.Services.Common;
using Store.DTO;
using Store.DTO.Common;

namespace Store.Application.Abstraction.IService
{
    public interface IProductService : IBaseServices<ProductDto>
    {
        OperationResult<List<ProductQueryDto>> Search(string filterValue, bool isXml);
    }
}
