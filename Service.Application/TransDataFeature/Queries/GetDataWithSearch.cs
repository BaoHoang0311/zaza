using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Application.Helper;
using Service.Domain.Models;
using X.PagedList;

namespace Service.Application.TransDataFeature.Queries
{
    public partial class GetSearchRequest : IRequest<IPagedList<TableDTOs>>
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgentName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public partial class GetSearchRequest : IRequest<IPagedList<TableDTOs>>
    {
        // Paging
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
    public class GetDataHandler : IRequestHandler<GetSearchRequest, IPagedList<TableDTOs>>
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;
        public GetDataHandler(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IPagedList<TableDTOs>> Handle(GetSearchRequest keySearch, CancellationToken cancellationToken)
        {
            var transactionDatas = _context.TransactionDatas
                                    .Where(x => x.ClosingAgtCeano == keySearch.AgentCEANO || x.ClosingAgtBizName == keySearch.AgentName)
                                    .AsQueryable();

            List<TableDTOs> tableDto = new List<TableDTOs>();

            IPagedList<TableDTOs> resTable;

            if (keySearch.op == 1)
            {
                var gceData = await transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate >= keySearch.From && x.SubDate <= keySearch.To)).ToListAsync();

                foreach (var item in gceData)
                {
                    if (item.TransactionGcedata != null)
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
                var gcrData = await transactionDatas.Include(m => m.TransactionGcrdatas.Where(x => x.RcvDate >= keySearch.From && x.RcvDate <= keySearch.To)).ToListAsync();

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
            if(keySearch.Page != null)
            {
                resTable = tableDto.AsQueryable().PageResultAsync(keySearch.Page.Value, keySearch.PageSize);
            }
            else
            {
                resTable = tableDto.AsQueryable().ExcelAsync();
            }
            return resTable;
        }
    }

}

