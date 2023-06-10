using Microsoft.AspNetCore.Mvc;
using NoName.Business.Services;
using NoName.WebUI.Models;
namespace NoName.WebUI.ViewComponents
{
	public class ProductsViewComponent : ViewComponent
	{
		private readonly IProductService _productService;
		public ProductsViewComponent(IProductService productService)
		{
			_productService = productService;
		}

		public IViewComponentResult Invoke()
		{
			var productDtos = _productService.GetProducts();

			var viewModel = productDtos.Select(x => new ProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
				UnitInStock = x.UnitInStock,
				UnitPrice = x.UnitPrice,			
				ImagePath = x.ImagePath
			}).ToList();



			return View(viewModel);

		}
	}
}
