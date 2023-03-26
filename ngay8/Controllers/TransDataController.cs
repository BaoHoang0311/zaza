using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.Helper;
using ngay8.ViewModels;
using Service.Application.TransDataFeature.Queries;

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
                    From = search.From.Value + new TimeSpan(00, 00, 01),
                    To = search.To.Value + new TimeSpan(23, 59, 59),
                    Page = search.Page,
                    PageSize = search.PageSize.Value,
                };

                var resSearch = await _mediator.Send(getSearchRequest);

                return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", resSearch);

            }
            return BadRequest("LỖI");
        }
        [HttpPost]

        public async Task<IActionResult> Excel([FromBody] SearchVM search)
        {
            if (ModelState.IsValid)
            {
                var getSearchRequest = new GetSearchRequest()
                {
                    op = search.op,
                    AgentCEANO = search.AgentCEANO,
                    AgentName = search.AgentName,
                    From = search.From.Value + new TimeSpan(00, 00, 01),
                    To = search.To.Value + new TimeSpan(23, 59, 59),
                    Page = null,
                };
                var resSearch = await _mediator.Send(getSearchRequest);

                if (resSearch.Count > 0)
                {
                    var exportbytes = resSearch.ToList().ExporttoExcel("abc");
                    return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "abc");
                }
                else
                {
                    TempData["Message"] = "No Data to Export";
                    return View();
                }
            }
            return null;
        }

    }
}
