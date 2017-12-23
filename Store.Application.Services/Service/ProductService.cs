using Store.DTO;
using Store.Repository;
using Store.Repository.IRepository;
using Store.Application.Abstraction.IService;
using Store.Entities.Tables;
using Store.DTO.Common;
using System.Collections.Generic;
using Store.Common.Helpers;
using Store.DTO.ProductXml;

namespace Store.Application.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(ProductDto dto)
        {
            var operation = new OperationResult();
            return !dto.IsXml ? AddToSql(dto, operation) : AddXml(dto, operation);
        }

        private OperationResult AddXml(ProductDto dto, OperationResult operation)
        {
            if (XmlHelper.GetByCode(dto.Code.Trim()))
            {
                operation.Messages.Add(new OperationMessage { Description = "Ya existe el producto en el archivo", Severity = 0 });
                return operation;
            }

            XmlHelper.Insert(new List<ProducXmlDto>
            {
                new ProducXmlDto { Code = dto.Code.Trim(), Name = dto.Name, Price = dto.Price, Stock = dto.Stock, CategoryCode = dto.CategoryCode }
            });
            return operation;
        }

        private OperationResult AddToSql(ProductDto dto, OperationResult operation)
        {
            if (_productRepository.GetByKey(dto.Code.Trim()) != null)
            {
                operation.Messages.Add(new OperationMessage { Description = "Ya existe el código del producto", Severity = 0 });
                return operation;
            }
            if (_categoryRepository.GetByKey(dto.CategoryCode) == null)
            {
                operation.Messages.Add(new OperationMessage { Description = "No existe el código de la categoria", Severity = 0 });
                return operation;
            }
            var entity = new Product
            {
                Code = dto.Code.Trim(),
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryCode = dto.CategoryCode
            };
            _productRepository.Insert(entity);
            _unitOfWork.Commit();

            return operation;
        }

        public OperationResult Delete(string code, bool isXml)
        {
            var operation = new OperationResult();
            if (isXml)
            {
                XmlHelper.Remove(code);
            }
            else
            {
                var entity = _productRepository.GetByKey(code);
                if (entity == null)
                {
                    operation.Messages.Add(new OperationMessage() { Description = "No se encuentra el codigo del producto", Severity = 0 });
                    return operation;
                }

                _productRepository.Delete(entity);
                _unitOfWork.Commit();
            }

            return operation;
        }

        public OperationResult Update(ProductDto dto)
        {
            var operation = new OperationResult();
            if (dto.IsXml)
            {
                if (!XmlHelper.GetByCode(dto.Code.Trim()))
                {
                    operation.Messages.Add(new OperationMessage { Description = "No se encuentra el codigo del producto", Severity = 0 });
                    return operation;
                }
                    
                XmlHelper.Update(dto.Code, new ProducXmlDto { Name = dto.Name, Price = dto.Price, Stock = dto.Stock, CategoryCode = dto.CategoryCode });
            }
            else
            {
                var entity = _productRepository.GetByKey(dto.Code.Trim());
                if (entity == null)
                {
                    operation.Messages.Add(new OperationMessage { Description = "No se encuentra el codigo del producto", Severity = 0 });
                    return operation;
                }
                if (_categoryRepository.GetByKey(dto.CategoryCode) == null)
                {
                    operation.Messages.Add(new OperationMessage { Description = "No existe el código de la categoria", Severity = 0 });
                    return operation;
                }

                entity.Code = dto.Code.Trim();
                entity.Name = dto.Name;
                entity.Price = dto.Price;
                entity.Stock = dto.Stock;
                entity.CategoryCode = dto.CategoryCode;
                _productRepository.Update(entity);
                _unitOfWork.Commit();
            }

            return operation;
        }

        public OperationResult<List<ProductQueryDto>> Search(string filterValue, bool isXml)
        {
            if (string.IsNullOrEmpty(filterValue))
                filterValue = string.Empty;
            var result = new List<ProductQueryDto>();
            if (isXml)
            {
                var oProducts = XmlHelper.Search(filterValue);
                if (oProducts != null)
                    foreach (var item in oProducts)
                    {
                        result.Add(new ProductQueryDto
                        {
                            Code = item.Code,
                            Name = item.Name,
                            CategoryCode = item.CategoryCode,
                            Price = item.Price,
                            CategoryName = item.CategoryName,
                            Stock = item.Stock,
                            IsXml = true
                        });
                    }
                return new OperationResult<List<ProductQueryDto>>(result);
            }

            var products = _productRepository.Search(filterValue);
            if (products != null)
                foreach (var item in products)
                {
                    result.Add(new ProductQueryDto
                    {
                        Code = item.Code,
                        Name = item.Name,
                        CategoryCode = item.CategoryCode,
                        Price = item.Price,
                        CategoryName = item.Category.Name,
                        Stock = item.Stock
                    });
                }

            return new OperationResult<List<ProductQueryDto>>(result);
        }
    }
}
