using Microsoft.AspNetCore.Mvc;
using ngay8.Data;
using static ngay8.Controllers.AutoCompleteController;

namespace ngay8.Controllers
{
    public class DragController : Controller
    {
        public static List<CountryJson> GetCountries()
        {
            return new List<CountryJson>() {
                new CountryJson(){id= 1, name = "Afghanistan"},
                new CountryJson(){id= 2, name = "Albania"},
                new CountryJson(){id= 3, name = "Algeria"},
                new CountryJson(){id= 4, name = "Andorra"},
                new CountryJson(){id= 5, name = "Angola"},
                new CountryJson(){id= 6, name = "Anguilla"},
            };
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestText()
        {
            var data = GetCountries();

            return PartialView("~/Views/Drag/_ModalTestDrag.cshtml",data);
        }
    }
}
