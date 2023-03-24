using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Domain.Models;
using X.PagedList;

namespace Service.Application.GraphDataFeature.Queries
{
    public class GetGraphRequest : IRequest<List<TableDTOs>>
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgentName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public class GetGraphHandler : IRequestHandler<GetGraphRequest, List<TableDTOs>>
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;
        public GetGraphHandler(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TableDTOs>> Handle(GetGraphRequest keySearch, CancellationToken cancellationToken)
        {
            var tableDto = new List<TableDTOs>();

            if (keySearch.op == 1)
            {
                tableDto = await _context.TransactionGcedatas
                    .Where(x => (x.AgtCeano == keySearch.AgentCEANO || x.AgtBizName == keySearch.AgentName) && x.SubDate >= keySearch.From && x.SubDate <= keySearch.To)
                    .Select(item1 => new TableDTOs
                    {
                        Id = item1.Id,
                        TransID = item1.TransId,
                        GrossValue = item1.GrossValue,
                        NetValue = item1.NetValue,
                        Date = item1.SubDate,
                        AgentName = item1.AgtBizName,
                        CEANo = item1.AgtCeano,
                    }).OrderBy(m => m.Date).ToListAsync();
            }
            else
            {
                tableDto = await _context.TransactionGcrdatas
                    .Where(x => (x.AgtCeano == keySearch.AgentCEANO || x.AgtBizName == keySearch.AgentName) && x.RcvDate >= keySearch.From && x.RcvDate <= keySearch.To)
                    .Select(item1 => new TableDTOs
                    {
                        Id = item1.Id,
                        TransID = item1.TransId,
                        GrossValue = item1.GrossValue,
                        NetValue = item1.NetValue,
                        Date = item1.RcvDate,
                        AgentName = item1.AgtBizName,
                        CEANo = item1.AgtCeano,
                    }).OrderBy(m => m.Date).ToListAsync();
            }
            return tableDto;
        }
    }

}

