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
        public class A
        {
            public int Id { get; set; }
            public List<GraphData> data { get; set; }
        }
        [HttpPost]
        public async Task<List<GraphData>> Report(SearchVM search)
        {
            if (!ModelState.IsValid) return null;
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
            var graphData = _mapper.Map<List<GraphData>>(resSearch.ToList());

            var dataCol = graphData.GroupBy(x => new { x.Month, x.Year }).Select(a => new GraphData
            {
                Month = a.Key.Month,
                Year = a.Key.Year,
                GrossValue = Math.Round(a.Sum(b => b.GrossValue),0),
                NetValue = Math.Round(a.Sum(c=> c.NetValue),0)
            }).ToList() ;
            return dataCol;
            //return PartialView("~/Views/Graph/_GraphPartialView.cshtml", graphData);
        }
    }
}
