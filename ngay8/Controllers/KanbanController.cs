using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ngay8.Data;
using ngay8.Models;
using System.Text;
using System;
using System.IO;

namespace ngay8.Controllers
{
    public class KanbanController : Controller
    {
        private static List<JsonToDoList> Get()
        {
            var path = "e:\\zaza\\ngay8\\Data\\datatodo.json";
            var str = System.IO.File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<List<JsonToDoList>>(str);
            return data;
        }
        private static void Save(List<JsonToDoList> data)
        {
            var path = "e:\\zaza\\ngay8\\Data\\datatodo.json";
            var content = JsonConvert.SerializeObject(data);
            System.IO.File.WriteAllText(path, content);
        }
        public IActionResult Index()
        {
            return View();
        }

        // PartialModal + GetData : Combo GetData xong render bằng partial View
        public IActionResult PartialModal()
        {
            return PartialView("~/Views/Kanban/_ModalTestKanban.cshtml");
        }
        public ActionResult GetData()
        {
            var data = Get();
            return Json(data);
        }

        public ActionResult InitAddData()
        {
            var data = Get();

            var model = new AddKanban();
            model.Assignee = data.Select(x => new SelectListItem { Text = x.Assignee, Value = x.Assignee }).ToList();

            var status = new List<string>() { "InProgress", "Open", "Testing", "Close" };

            model.Status = status.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
            return PartialView("~/Views/Kanban/Partial/_ModalAddKanban.cshtml", model);
        }
        public ActionResult Insert(JsonToDoList value)
        {
            var data = Get();

            var maxid = data.Max(x => x.Id);

            
            data.Add(new JsonToDoList() { Id = maxid + 1, Assignee = value.Assignee, Status = value.Status, Summary = value.Summary });

            Save(data);

            return Json(new { result = "success" });
        }

        public ActionResult Update(JsonToDoList value)
        {
            var data = Get();
            var idx = data.FindIndex(x => x.Id == value.Id);
            data[idx].Status = value.Status;
            data[idx].Summary = value.Summary;
            data[idx].Assignee = value.Assignee;
            Save(data);
            return Json(new { result = "success" });
        }
        public ActionResult Delete(int Id)
        {
            var data = Get();
            var idx = data.FindIndex(x => x.Id == Id);

            data.RemoveAt(idx);
            Save(data);
            return Json(new { result = "success" });
        }
    }
}
