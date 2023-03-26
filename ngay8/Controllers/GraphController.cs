using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.Helper;
using ngay8.Models;
using ngay8.ViewModels;
using Service.Application.GraphDataFeature.Queries;
using Service.Domain.Models;
using static Service.Application.GraphDataFeature.Queries.GetGraphWithStoreProcedure;

namespace ngay8.Controllers
{
    public class GraphController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GraphController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Analytics()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Report(SearchVM search)
        {
            if (!ModelState.IsValid) return Json(new { status = "fail" });
            var getGraphRequest = new GetGraphRequest()
            {
                op = search.op,
                AgentName = search.AgentName,
                AgentCEANO = search.AgentCEANO,
                From = search.From.Value + new TimeSpan(00, 00, 02),
                To = search.To.Value + new TimeSpan(23, 59, 59),
            };

            var resSearch = await _mediator.Send(getGraphRequest);
            var graphData = _mapper.Map<List<GraphData>>(resSearch.ToList());

            var dataCol = graphData.GroupBy(x => new { x.Month, x.Year }).Select(a => new GraphData
            {
                Month = a.Key.Month,
                Year = a.Key.Year,
                GrossValue = Math.Round(a.Sum(b => b.GrossValue), 0),
                NetValue = Math.Round(a.Sum(c => c.NetValue), 0)
            }).ToList();

            if (dataCol == null || dataCol.Count() == 0) return Json(new { status = "fail" });
            return Json(new { status = "success", data = dataCol });
        }
        [HttpPost]
        public async Task<JsonResult> ReportProcedure(SearchVM search)
        {
            if (!ModelState.IsValid) return Json(new { status = "fail" });
            var getGraphRequest = new GetGraphWithStoreProcedureRequest()
            {
                op = search.op,
                AgentName = search.AgentName ?? "",
                AgentCEANO = search.AgentCEANO ?? "",
                From = search.From.Value + new TimeSpan(00, 00, 02),
                To = search.To.Value + new TimeSpan(23, 59, 59),
            };

            var graphDataPro = await _mediator.Send(getGraphRequest);

            if (graphDataPro == null) return Json(new { status = "fail" });
            var dataCol = graphDataPro.GroupBy(x => new { x.Month, x.Year })
                .Select(a => new GraphDataPro
                {
                    Month = a.Key.Month,
                    Year = a.Key.Year,
                    GrossValue = Math.Round(a.Sum(b => b.GrossValue), 0),
                    NetValue = Math.Round(a.Sum(c => c.NetValue), 0)
                }).ToList();

            if (dataCol == null || dataCol.Count() == 0) return Json(new { status = "fail" });
            return Json(new { status = "success", data = dataCol });
        }
        public IActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ExportExcel([FromBody] ClosexmlVM closeXml)
        {
            if (!ModelState.IsValid) return Json(new { status = "fail" });
            var getExcelwithDate = new GetExcelwithDate()
            {
                From = closeXml.From.Value + new TimeSpan(00, 00, 02),
                To = closeXml.To.Value + new TimeSpan(23, 59, 59),
            };
            var top10KeyInDate = await _mediator.Send(getExcelwithDate);
            var exportbyte = top10KeyInDate.ExporttoExcelCloseXML(getExcelwithDate);
            return File(exportbyte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}


