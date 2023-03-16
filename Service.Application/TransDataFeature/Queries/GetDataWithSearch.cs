using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Application.DTOs;
using Service.Domain.Models;
using System.Security.Cryptography;

namespace Service.Application.TransDataFeature.Queries
{
    public class GetSearchDetail : IRequest<List<TransactionData>>
    {
        public int op { get; set; }
        public string? AgentCEANO { get; set; }
        public string? AgentName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public class GetDataWithSearchDetail : IRequestHandler<GetSearchDetail, List<TransactionData>>
    {
        private readonly AestrainingContext _context;
        public GetDataWithSearchDetail(AestrainingContext context)
        {
            _context = context;
        }
        public async Task<List<TransactionData>> Handle(GetSearchDetail keySearch, CancellationToken cancellationToken)
        {
            var transactionDatas = _context.TransactionDatas.Where(x => x.ClosingAgtCeano == keySearch.AgentCEANO || x.ClosingAgtCeano == keySearch.AgentName).Take(10).AsQueryable();

            if (keySearch.op == 1)
            {

                if (keySearch.From != null && keySearch.To != null)
                {
                    transactionDatas = transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate  >= keySearch.From && x.SubDate <= keySearch.To)).Take(10);

                    //transactionDatas = transactionDatas.
                }
                else if ( keySearch.From != null && keySearch.To == null ) transactionDatas = transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate >= keySearch.From)).Take(10);
                else if (keySearch.From == null || keySearch.To != null ) transactionDatas = transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate <= keySearch.To)).Take(10);
            }
            else
            {
                if (keySearch.From != null && keySearch.To != null)
                {
                    transactionDatas = transactionDatas.Include(m => m.TransactionGcrdatas.Where(x => x.RcvDate >= keySearch.From && x.RcvDate <= keySearch.From)).Take(10);
                }
                else if (keySearch.From != null && keySearch.To == null) transactionDatas = transactionDatas.Include(m => m.TransactionGcrdatas.Where(x => x.RcvDate >= keySearch.From)).Take(10);
                else if (keySearch.From == null || keySearch.To != null) transactionDatas = transactionDatas.Include(m => m.TransactionGcrdatas.Where(x => x.RcvDate <= keySearch.To)).Take(10);
            }


            //var res = _context.TransactionDatas.Where(a => a.TransId == id).FirstOrDefault();
            //;
            //_context.Entry(res).Collection(res => res.TransactionGcedatas).Load();
            //_context.Entry(res).Collection(res => res.TransactionGcrdatas).Load();
            //return res;


            //var data = await _context.TransactionDatas.Where(x => x.ClosingAgtBizName == keySearch.AgentName).ToListAsync();
            var zzz = await transactionDatas.ToListAsync();
            return zzz;
        }
    }
}
