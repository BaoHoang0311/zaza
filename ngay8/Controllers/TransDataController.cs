using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.Models;
using PagedList.Core;
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
        public async Task<IActionResult> Index(SearchVM search, int? page)
        {
            if (ModelState.IsValid)
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 2;
                var getSearchRequest = new GetSearchRequest()
                {
                    op = search.op,
                    AgentName = search.AgentName,
                    AgentCEANO = search.AgentCEANO,
                    From = search.From,
                    To = search.To,
                };
                ViewBag.CurrentPage = page;
                ViewBag.KKK = search;
                var z = await _mediator.Send(getSearchRequest);
                PagedList<TableDTOs> models = new PagedList<TableDTOs>(z.AsQueryable(), pageNumber, pageSize);

                return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", models);

            }
            return BadRequest("LỖI");
            //return View("Index");
        }
    }
}
