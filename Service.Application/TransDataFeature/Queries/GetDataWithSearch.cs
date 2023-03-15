using AutoMapper;
using MediatR;
using Service.Application.DTOs;
using Service.Application.ProductFeatures.Commands;
using Service.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.TransDataFeature.Queries
{
    public record SearchDetail : IRequest
    {
        public bool GCE { get; set; }
        public bool GCR { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgnetName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime Received { get; set; }
    }

    public class GetDataWithSearch
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;

        public GetDataWithSearch(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DataCustom>> Handle(SearchDetail keySearch, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
