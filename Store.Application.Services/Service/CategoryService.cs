using System.Collections.Generic;
using Store.Application.Abstraction.IService;
using Store.DTO;
using Store.DTO.Common;
using Store.Repository.IRepository;

namespace Store.Application.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public OperationResult<List<CategoryDto>> Search(string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                filterValue = string.Empty;
            var categories = _categoryRepository.Search(filterValue);
            var result = new List<CategoryDto>();
            categories?.ForEach(item =>
            {
                result.Add(new CategoryDto { Code = item.Code, Name = item.Name });
            });

            return new OperationResult<List<CategoryDto>>(result);

        }
    }
}
