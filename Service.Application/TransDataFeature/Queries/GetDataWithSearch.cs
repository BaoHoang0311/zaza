using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Service.Application.TransDataFeature.Queries
{
    public class GetSearchRequest : IRequest<List<TableDTOs>>
    {
        public int op { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]

        public string? AgentCEANO { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]

        public string? AgentName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]

        public DateTime? From { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập")]

        public DateTime? To { get; set; }
    }
    public class GetDataHandler : IRequestHandler<GetSearchRequest, List<TableDTOs>>
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;
        public GetDataHandler(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TableDTOs>> Handle (GetSearchRequest keySearch, CancellationToken cancellationToken)
        {
            var transactionDatas = _context.TransactionDatas
                                    .Where(x => x.ClosingAgtCeano == keySearch.AgentCEANO || x.ClosingAgtBizName == keySearch.AgentName)
                                    .AsQueryable();

            List<TableDTOs> tableDto = new List<TableDTOs>();

            if (keySearch.op == 1)
            {
                tableDto = await transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate >= keySearch.From && x.SubDate <= keySearch.To))
                                            .Select(o => new TableDTOs()
                                            {
                                                TransID = o.TransId,
                                                ProjectNane = o.ProjectName,
                                                TransactedPrice = o.TransactedPrice,
                                                TransactedCol = o.TransactionComm,
                                                Gcedata = o.TransactionGcedata.ToList(),
                                                Gcrdata = o.TransactionGcrdatas.ToList(),
                                            }).OrderBy(o=>o.TransID).AsNoTracking().ToListAsync();


            }
            else
            {
                tableDto = await transactionDatas.Include(m => m.TransactionGcrdatas.Where(x => x.RcvDate >= keySearch.From && x.RcvDate <= keySearch.From))
                                            .Select(o => new TableDTOs
                                            {
                                                TransID = o.TransId,
                                                ProjectNane = o.ProjectName,
                                                TransactedPrice = o.TransactedPrice,
                                                TransactedCol = o.TransactionComm,
                                                Gcrdata = o.TransactionGcrdatas.ToList(),
                                            }).OrderBy(o => o.TransID).AsNoTracking().ToListAsync(); 
            }

            return tableDto;
        }
    }
}
