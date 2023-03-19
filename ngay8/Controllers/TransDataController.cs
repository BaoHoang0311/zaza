using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ngay8.Models;
using Service.Application.DTOs;
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
        public async Task<IActionResult> Index(SearchVM search)
        {
            if (ModelState.IsValid)
            {
                var getSearchRequest = new GetSearchRequest()
                {
                    op = search.op,
                    AgentName = search.AgentName,
                    AgentCEANO = search.AgentCEANO,
                    From = search.From,
                    To = search.To,
                };
                var z = await _mediator.Send(getSearchRequest);
                return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml",z);
            }
            //return BadRequest("LỖI"); 
            return View("Index");
        }
        public async Task<IActionResult> Paging(List<TableDTOs> data, int pageSize, int page = 1)
        {
            var dt = data.Skip((page - 1) * pageSize).Take(pageSize);
            return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", dt);
        }
    }
}
