using Microsoft.AspNetCore.Mvc;
using ngay8.ViewModels;

namespace ngay8.Controllers
{
    public class PondFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Product product, IFormFile[] photos)
        {
            return View();
        }
    }
}
