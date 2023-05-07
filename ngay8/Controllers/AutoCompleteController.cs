using Microsoft.AspNetCore.Mvc;
using Service.Domain.Models;
using ngay8.Data;
namespace ngay8.Controllers
{
    public class AutoCompleteController : Controller
    {
        private readonly AestrainingContext _context;
        public AutoCompleteController(AestrainingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAuto(string querytext)
        {
            var country_list = new List<CountryJson>() {
                new CountryJson(){id= 1, name = "Afghanistan"},
                new CountryJson(){id= 2, name = "Albania"},
                new CountryJson(){id= 3, name = "Algeria"},
                new CountryJson(){id= 4, name = "Andorra"},
                new CountryJson(){id= 5, name = "Angola"},
                new CountryJson(){id= 6, name = "Anguilla"},
            };
            //var country_list = new List<string>() { "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla" };
            return Json(country_list);
        }
    }
}
