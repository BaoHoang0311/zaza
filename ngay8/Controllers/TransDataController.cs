using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ngay8.Helper;
using ngay8.ViewModels;
using Service.Application.TransDataFeature.Queries;
using Service.Domain.Models;
namespace ngay8.Controllers
{
    public class TransDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        private readonly AestrainingContext _context;
        private readonly ILogger<TransDataController> _logger;
        public TransDataController(IMapper mapper, IMediator mediator, IUriService uriService, AestrainingContext context, ILogger<TransDataController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _uriService = uriService;
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> TransGce(PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;
            var res = await PagedResponse<TransactionGcedata>.CreatePagedReponse(_context.TransactionGcedatas.ToList(), _uriService, route, paginationFilter);
            ViewBag.Data = JsonConvert.SerializeObject(res);
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            // terminal chọn cái project run là ra
            _logger.LogWarning("hello");
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
                    To = search.To.Value.AddDays(1),
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
