using Microsoft.AspNetCore.Mvc.Rendering;
using ngay8.Data;

namespace ngay8.Models
{
    public class AddKanban
    {
        public List<SelectListItem> Assignee { get;set;} = new List<SelectListItem>();
        public List<SelectListItem> Status { get;set;} = new List<SelectListItem>();
    }
}
