using Microsoft.AspNetCore.Mvc;

namespace ngay8.Controllers
{
    public class RateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Moodal()
        {
            return PartialView("~/Views/Rate/Partial/_RatePartial.cshtml");
        }
    }
}
