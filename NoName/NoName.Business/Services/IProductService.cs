using NoName.Business.Dtos;
using NoName.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoName.Business.Services
{
    public interface IProductService
    {
        ServiceMessage AddProduct(AddProductDto addProductDto);

        List<ProductDto> GetProducts();

        EditProductDto GetProductById(int id);

        void UpdateProduct(EditProductDto editProductDto);

        void DeleteProduct(int id);
        DetailProductDto GetProductDetail(int id);
    }
}
