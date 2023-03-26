using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Domain.Models;
using System.Data;
using System.Text;

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
                    new SqlParameter("@op", keySearch.op),
                    new SqlParameter("@AgentCEANO", keySearch.AgentCEANO ),
                    new SqlParameter("@AgentName",keySearch.AgentName ?? ""),
                    new SqlParameter("@From", keySearch.From.ToString("dd-MMM-yyyy HH:mm:ss")),
                    new SqlParameter("@To", keySearch.To.ToString("dd-MMM-yyyy HH:mm:ss")),
                    new SqlParameter("@result",ParameterDirection.Output)
                };

                // lấy bằng SP nhưng phải dk trong db context
                //string strStorePro = "exec GetGraph @op, @AgentCEANO, @AgentName, @From, @To";
                //var result1 = await _context.graphData.FromSqlRaw(strStorePro, parameter.ToArray()).ToListAsync();
                //var graphData = result1.Select(item1 => new GraphDataPro
                //{
                //    Month = item1.Month,
                //    Year = item1.Year,
                //    GrossValue = item1.GrossValue,
                //    NetValue = item1.NetValue
                //}).ToList();

                // lấy bằng SP KO phải dk trong db context => trả về json
                var result1 = _context.Database.SqlQuery<string>($"Exec Datafunc1 {parameter[0]},{parameter[1]},{parameter[2]} , {parameter[3]},{parameter[4]}");
                var s = new StringBuilder();
                foreach (var item in result1)
                {
                    s.Append(item);
                }
                var graphData = JsonConvert.DeserializeObject<List<GraphDataPro>>(s.ToString());

                return graphData;
            }
        }

    }
}
