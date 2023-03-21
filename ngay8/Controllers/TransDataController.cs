using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.Models;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using Service.Application.TransDataFeature.Queries;
using Service.Application.DTOs;
using System.Data;
using System.Reflection;

namespace ngay8.Controllers
{
    public class TransDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TransDataController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchVM search)
        {
            if (ModelState.IsValid)
            {
                var getSearchRequest = new GetSearchRequest()
                {
                    op = search.op,
                    AgentName = search.AgentName,
                    AgentCEANO = search.AgentCEANO,
                    From = search.From.Value,
                    To = search.To,
                    Page = search.Page.Value,
                    PageSize = search.PageSize.Value,

                };

                var resSearch = await _mediator.Send(getSearchRequest);

                return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", resSearch);

            }
            return BadRequest("LỖI");
            //return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DownloadReport(SearchVM search)
        {

            var getSearchRequest = new GetSearchRequest()
            {
                op = 1,
                AgentCEANO = "207607465H",
                AgentName = null,
                From = new DateTime(2016, 01, 01),
                To = new DateTime(2023, 01, 01),
                Page = 1,
                PageSize = 25,
            };
            var resSearch = await _mediator.Send(getSearchRequest);

            string reportname = $"User_Wise.xlsx";

            if (resSearch.Count > 0)
            {
                var exportbytes = ExporttoExcel(resSearch.ToList(), reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                TempData["Message"] = "No Data to Export";
                return View();
            }
        }
        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            List<string[]> headerRow = new List<string[]>()
            {
                new string[] { "ID", "TransID", "Agent Name", "CEANo", "GrossValue", "NetValue", "Date", "ProjectNane", "TransactedPrice", "TransactedCol" }
            };
            var membersToShow = typeof(TableDTOs).GetMember().Where(p => headerRow.Contains(p[0].Name)).ToArray();
            ws.Cells["A2"].LoadFromCollection(table, true, TableStyles.Light1, BindingFlags.Default, membersToShow);
            return pack.GetAsByteArray();
        }
    }
}
