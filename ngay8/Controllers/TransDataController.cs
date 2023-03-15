using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Search(SearchDetail search )
        {
            var z = await _mediator.Send(new GetProductByIdQuery() { Id=5});
            Console.WriteLine("haha");
            return View();
        }
    }
}
