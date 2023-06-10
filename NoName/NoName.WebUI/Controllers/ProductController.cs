using Microsoft.AspNetCore.Mvc;
using NoName.Business.Services;
using NoName.WebUI.Models;

namespace NoName.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Detail(int id)
        {
            var productDto = _productService.GetProductDetail(id);

            var response = new ProductDetailViewModel()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImagePath = productDto.ImagePath
            };

            return View(response);
        }
    }
}
