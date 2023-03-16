using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ngay8.ViewModel;
using Service.Application.ProductFeatures.Commands;
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchDetailVM search )
        {
            //var z = await _mediator.Send(new GetAllProductsQuery());

            var z = await _mediator.Send(new GetSearchDetail()
            {
                op = search.op,
                AgentName = search.AgentName,
                AgentCEANO = search.AgentCEANO,
                From = search.From,
                To = search.To,
            });
            return PartialView("~/Views/TransData/_SearchResultPartialView.cshtml", z);
        }
    }
}
