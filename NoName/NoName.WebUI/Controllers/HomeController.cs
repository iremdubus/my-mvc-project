using Microsoft.AspNetCore.Mvc;

namespace NoName.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
