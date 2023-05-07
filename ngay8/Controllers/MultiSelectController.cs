using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ngay8.Models;
using System.Diagnostics;
using ngay8.Data;
using static ngay8.Controllers.AutoCompleteController;

namespace ngay8.Controllers
{
    
    public class MultiSelectController : Controller
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
            ViewBag.ddlYear = GetCountries().Select(x => new SelectListItem { Text = x.name, Value = x.id.ToString() }).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
