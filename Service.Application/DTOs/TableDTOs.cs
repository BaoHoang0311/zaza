using Service.Domain.Models;

namespace Service.Application.DTOs
{
    public class TableDTOs
    {
        public TableDTOs()
        {
            Gcedata = new();
            Gcrdata = new();
        }
        public int TransID { get; set; }
        public string? AgentName { get; set; }
        public string? CEANo { get; set; }
        public double? GrossVlaue { get; set; }
        public double? NetValue { get; set; }

        public string? ProjectNane { get; set; }
        public double? TransactedPrice { get; set; }
        public double? TransactedCol { get; set; }

        public List<TransactionGcedata> Gcedata { get; set; }
        public List<TransactionGcrdata> Gcrdata { get; set; }
    }
}
