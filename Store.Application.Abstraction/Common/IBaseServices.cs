using Store.DTO.Common;

namespace Store.Application.Services.Common
{
    public interface IBaseServices<T>
    {
        OperationResult Create(T dto);
        OperationResult Update(T dto);
        OperationResult Delete(string code, bool isXml = false);
    }
}
