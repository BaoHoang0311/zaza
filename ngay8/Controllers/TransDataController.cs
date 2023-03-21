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
using OfficeOpenXml.Attributes;
using Service.Application.Helper;

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
                    Page = search.Page,
                    PageSize = search.PageSize.Value,

                };

                var resSearch = await _mediator.Send(getSearchRequest);

                return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", resSearch);

            }
            //return BadRequest("LỖI");
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Excel([FromBody] SearchVM search)
        {
            if (ModelState.IsValid)
            {
                var getSearchRequest = new GetSearchRequest()
                {
                    op = 1,
                    AgentCEANO = search.AgentCEANO,
                    AgentName = search.AgentName,
                    From = search.From,
                    To = search.To,
                    Page = null,
                };
                var resSearch = await _mediator.Send(getSearchRequest);

                if (resSearch.Count > 0)
                {
                    var exportbytes = resSearch.ToList().ExporttoExcel(search.fileName);
                    return File(exportbytes, "application/octet-stream");
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
