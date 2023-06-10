using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoName.Business.Dtos;
using NoName.Business.Services;
using NoName.WebUI.Areas.Admin.Models;
using System.Data;

namespace NoName.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
       
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        public ProductController( IProductService productService, IWebHostEnvironment environment)
        {
            
            _productService = productService;
            _environment = environment;
        }
        public IActionResult List()
        {
            var productDtos = _productService.GetProducts();

            var viewModel = productDtos.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                ImagePath = x.ImagePath
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("Form", new ProductFormViewModel());
        }

        [HttpPost]
        public IActionResult Save(ProductFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
          
                return View("Form", formData);
            }

            var newFileName = "";

            if (formData.File != null)
            {
                var allowedFileContentTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif" };

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" };

                var fileContentType = formData.File.ContentType;
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName);

                var fileExtension = Path.GetExtension(formData.File.FileName);

                if (!allowedFileContentTypes.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
                {
                   

                    ViewBag.FileError = "Dosya formatı veya içeriği hatalı.";

                    
                    return View("Form", formData);
                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;


                var folderPath = Path.Combine("images", "products");
                

                var wwwRootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                

                var wwwRootFilePath = Path.Combine(wwwRootFolderPath, newFileName);

                

                Directory.CreateDirectory(wwwRootFolderPath);

                using (var fileStream = new FileStream(wwwRootFilePath, FileMode.Create))
                {
                    formData.File.CopyTo(fileStream);
                }
               

            }


            if (formData.Id == 0) 
            {

                var addProductDto = new AddProductDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description,
                    UnitInStock = formData.UnitInStock,                 
                    ImagePath = newFileName
                };

                var response = _productService.AddProduct(addProductDto);

                if (response.IsSucceed)
                {
                    return RedirectToAction("List");

                }
                else
                {
                    ViewBag.ErrorMessage = response.Message;
                   
                    return View("Form", formData);

                }

            }
            else 
            {

                var editProductDto = new EditProductDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitInStock = formData.UnitInStock,   
                };

                if (formData.File != null)
                    editProductDto.ImagePath = newFileName;

                _productService.UpdateProduct(editProductDto);

            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editProductDto = _productService.GetProductById(id);

            var viewModel = new ProductFormViewModel()
            {
                Id = editProductDto.Id,
                Name = editProductDto.Name,
                Description = editProductDto.Description,
                UnitInStock = editProductDto.UnitInStock,
    
            };
   
            return View("Form", viewModel);
        }


        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return RedirectToAction("List");
        }
    }
}
