using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.Models;
using ngay8.ViewModels;
using Service.Application.TransDataFeature.Queries;

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
        [HttpPost]
        public async Task<IActionResult> Report(SearchVM search)
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
                    Page = null,
                };

                var resSearch = await _mediator.Send(getSearchRequest);
                //var graphData = _mapper.Map<List<GraphData>>(resSearch.ToList());
                var graphData = new List<GraphData>() {
                    new GraphData() { Year= 2016,GrossValue = 29173,NetValue = 29173 },
                    new GraphData() { Year= 2017,GrossValue = 29173,NetValue = 29173 },
                    new GraphData() { Year= 2018,GrossValue = 29173,NetValue = 29173 },
                    new GraphData() { Year= 2019,GrossValue = 29173,NetValue = 29173 },
                    new GraphData() { Year= 2020,GrossValue = 29173,NetValue = 29173},
                    };
                //return PartialView("~/Views/Graph/_GraphPartialView.cshtml", graphData);
                //return PartialView("~/Views/Graph/_GraphPartialView.cshtml");
                return RedirectToAction("Index", "Home");

            }
            return BadRequest("LỖI");
            //return View();
        }
    }
}
