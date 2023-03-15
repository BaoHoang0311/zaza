using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Application.TransDataFeature.Queries;

namespace ngay8.Controllers
{
    public class TransDataController : Controller
    {
        private readonly IMapper _mapper;

        private IMediator _mediator;
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
        public IActionResult Search(SearchDetail search )
        {
            return View();
        }
    }
}
