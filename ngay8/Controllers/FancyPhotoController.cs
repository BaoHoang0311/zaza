using Microsoft.AspNetCore.Mvc;

namespace ngay8.Controllers
{
    public class FancyPhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
