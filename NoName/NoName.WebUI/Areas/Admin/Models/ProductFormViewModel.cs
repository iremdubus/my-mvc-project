using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NoName.WebUI.Areas.Admin.Models
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Ürün ismi girmek zorunludur.")]
        public string Name { get; set; }


        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int UnitInStock { get; set; }

        [Display(Name = "Ürün Görseli")]
        public IFormFile? File { get; set; }   

    }
}
