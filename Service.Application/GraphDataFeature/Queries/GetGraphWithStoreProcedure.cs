using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.Domain.Models;

namespace Service.Application.GraphDataFeature.Queries
{
    public class GetGraphWithStoreProcedure
    {
        public class GetGraphWithStoreProcedureRequest : IRequest<List<GraphDataPro>>
        {
            public int op { get; set; }
            public string? AgentCEANO { get; set; }
            public string? AgentName { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
        public class GetGraphWithStoreProcedureHandler : IRequestHandler<GetGraphWithStoreProcedureRequest, List<GraphDataPro>>
        {
            private readonly AestrainingContext _context;
            private readonly IMapper _mapper;
            public GetGraphWithStoreProcedureHandler(AestrainingContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<GraphDataPro>> Handle(GetGraphWithStoreProcedureRequest keySearch, CancellationToken cancellationToken)
            {
                var parameter = new List<SqlParameter>()
                {
                    new SqlParameter("@op", 1),
                    new SqlParameter("@AgentCEANO", keySearch.AgentCEANO),
                    new SqlParameter("@AgentName",keySearch.AgentName ?? ""),
                    new SqlParameter("@From", keySearch.From.ToString("dd-MMM-yyyy HH:mm:ss")),
                    new SqlParameter("@To", keySearch.To.ToString("dd-MMM-yyyy HH:mm:ss"))
                };
                string strStorePro = "exec GetGraph @op, @AgentCEANO, @AgentName, @From, @To";
                var result1 = await _context.graphData.FromSqlRaw(strStorePro, parameter.ToArray()).ToListAsync();

                var graphData = result1.Select(item1 => new GraphDataPro
                {
                    Month = item1.Month,
                    Year = item1.Year,
                    GrossValue = item1.GrossValue,
                    NetValue = item1.NetValue
                }).OrderBy(m => m.Year).ToList();

                return graphData;
            }
        }

    }
}
