using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Service.Application.TransDataFeature.Queries
{
    public class GetSearchRequest : IRequest<List<TransactionData>>
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
    public class GetDataHandler : IRequestHandler<GetSearchRequest, List<TransactionData>>
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;
        public GetDataHandler(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TransactionData>> Handle(GetSearchRequest keySearch, CancellationToken cancellationToken)
        {
            var transactionDatas = _context.TransactionDatas.Where(x => x.ClosingAgtCeano == keySearch.AgentCEANO || x.ClosingAgtCeano == keySearch.AgentName).Take(10).AsQueryable();

            if (keySearch.op == 1)
            {

                if (keySearch.From != null && keySearch.To != null)
                {
                    transactionDatas = transactionDatas.Include(m => m.TransactionGcedata.Where(x => x.SubDate  >= keySearch.From && x.SubDate <= keySearch.To)).Take(10);

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
