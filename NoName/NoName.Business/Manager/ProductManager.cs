using NoName.Business.Dtos;
using NoName.Business.Services;
using NoName.Business.Types;
using NoName.Data.Entities;
using NoName.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoName.Business.Manager
{
    public class ProductManager : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;

        public ProductManager(IRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public ServiceMessage AddProduct(AddProductDto addProductDto)
        {
            var hasProduct = _productRepository.GetAll(x => x.Name.ToLower() == addProductDto.Name.ToLower()).ToList();

            if (hasProduct.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu isimde bir ürün zaten mevcut."
                };
            }

            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                ImagePath = addProductDto.ImagePath
            };

            _productRepository.Add(productEntity);

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                
            };

            return editProductDto;
        }

        public DetailProductDto GetProductDetail(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var productDto = new DetailProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                ImagePath = productEntity.ImagePath,
            };

            return productDto;
        }

        public List<ProductDto> GetProducts()
        {
            var productEntites = _productRepository.GetAll().OrderBy(x => x.Name);

            var productDtoList = productEntites.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                ImagePath = x.ImagePath
            }).ToList();

            return productDtoList;
        }

        public void UpdateProduct(EditProductDto editProductDto)
        {
            var productEntity = _productRepository.GetById(editProductDto.Id);

            productEntity.Name = editProductDto.Name;
            productEntity.Description = editProductDto.Description;
            productEntity.UnitInStock = editProductDto.UnitInStock;
            productEntity.UnitPrice = editProductDto.UnitPrice;
      
            if (editProductDto.ImagePath is not null)
                productEntity.ImagePath = editProductDto.ImagePath;

            _productRepository.Update(productEntity);

        }
    }
}
