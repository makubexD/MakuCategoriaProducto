using Store.Application.Abstraction.IService;
using Store.DTO;
using Store.DTO.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace Store.WebAPI.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, Route("search")]
        public OperationResult<List<CategoryDto>> Search(string filterValue)
        {
            return _categoryService.Search(filterValue);
        }
    }
}
