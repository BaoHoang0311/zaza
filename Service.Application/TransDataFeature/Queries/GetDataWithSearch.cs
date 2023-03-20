using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace Service.Application.TransDataFeature.Queries
{

    public class GetSearchRequest : IRequest<List<TableDTOs>>
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgentName { get; set; }
        public DateTime? From { get; set; }
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
                var gceData = await transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate >= keySearch.From && x.SubDate <= keySearch.To)).ToListAsync();

                foreach (var item in gceData)
                {
                    if(item.TransactionGcedata != null)
                    {
                        foreach (var item1 in item.TransactionGcedata)
                        {
                            tableDto.Add(new TableDTOs
                            {
                                ProjectNane = item.ProjectName,
                                TransactedPrice = item.TransactedPrice,
                                TransactedCol = item.TransactionComm,
                                
                                Id = item1.Id,
                                TransID = item1.TransId,
                                GrossValue = item1.GrossValue,
                                NetValue = item1.NetValue,
                                Date = item1.SubDate,
                                AgentName = item1.AgtBizName,
                                CEANo = item1.AgtCeano,
                            });
                        }
                    }
                }

            }
            else
            {
                var gcrData = await transactionDatas.Include(m => m.TransactionGcrdatas.Where(x =>x.RcvDate >= keySearch.From && x.RcvDate <= keySearch.To)).ToListAsync();

                foreach (var item in gcrData)
                {
                    if (item.TransactionGcrdatas != null)
                    {
                        foreach (var item1 in item.TransactionGcrdatas)
                        {
                            tableDto.Add(new TableDTOs
                            {
                                ProjectNane = item.ProjectName,
                                TransactedPrice = item.TransactedPrice,
                                TransactedCol = item.TransactionComm,

                                Id = item1.Id,
                                TransID = item1.TransId,
                                GrossValue = item1.GrossValue,
                                NetValue = item1.NetValue,
                                Date = item1.RcvDate,
                                AgentName = item1.AgtBizName,
                                CEANo = item1.AgtCeano,
                            });
                        }
                    }
                }
            }

            return tableDto.OrderBy(m=> m.TransID).ToList();
        }
    }
}
