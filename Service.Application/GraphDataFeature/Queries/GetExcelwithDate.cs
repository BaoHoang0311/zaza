using AutoMapper;
using MediatR;
using Service.Domain.Models;

namespace Service.Application.GraphDataFeature.Queries
{
    public class ProjectAnalytics
    {
        public int Index { get; set; }
        public string? ProjectName { get; set; }
        public double TransactedPrice { get; set; }
    }
    public class GetExcelwithDate : IRequest<List<ProjectAnalytics>>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    public class GetExcelwithDateHandler : IRequestHandler<GetExcelwithDate, List<ProjectAnalytics>>
    {
        private readonly AestrainingContext _context;
        private readonly IMapper _mapper;

        public GetExcelwithDateHandler(AestrainingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProjectAnalytics>> Handle(GetExcelwithDate keySearch, CancellationToken cancellationToken)
        {
            int i = 0;
            var transactionDatas = _context.TransactionDatas
                .Where(x => x.ProjectName != null && x.ProjectName != "" && x.KeyinDate >= keySearch.From && x.KeyinDate <= keySearch.To)
                ?.GroupBy(x => new { x.ProjectName })
                .Select(x => new ProjectAnalytics
                {
                    ProjectName = x.Key.ProjectName,
                    TransactedPrice = Math.Round(x.Sum(a => a.TransactedPrice), 0)
                }).OrderByDescending(q => q.TransactedPrice).Take(10).ToList();

            var zz = transactionDatas.Select((x, index) => new ProjectAnalytics { Index = ++index, ProjectName = x.ProjectName, TransactedPrice = x.TransactedPrice }).ToList();

            return zz;
        }
    }
}