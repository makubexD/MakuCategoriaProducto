using Store.Application.Abstraction.IService;
using Store.DTO;
using Store.DTO.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace Store.WebAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productServices;
        public ProductController(IProductService productServices)
        {
            _productServices = productServices;
        }

        [HttpPost, Route]
        public OperationResult Create([FromBody]ProductDto model)
        {
            if (ModelState.IsValid)
                return _productServices.Create(model);
            return ToOperationResult(model);
        }

        [HttpPut, Route]
        public OperationResult Update([FromBody]ProductDto model)
        {
            if (ModelState.IsValid)
                return _productServices.Update(model);
            return ToOperationResult(model);
        }

        [HttpDelete, Route("{code}/{isXml}")]
        public OperationResult Delete(string code, bool isXml= false)
        {
            return _productServices.Delete(code, isXml);
        }

        [HttpGet, Route("search/{isXml}")]
        public OperationResult<List<ProductQueryDto>> Search(string filterValue, bool isXml = false)
        {
            return _productServices.Search(filterValue, isXml);
        }
    }
}
