using OfficeOpenXml.Attributes;
using Service.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Service.Application.DTOs
{
    [EpplusTable]
    public class TableDTOs
    {
        public TableDTOs()
        {
            Gcedata = new();
            Gcrdata = new();
        }
        public long Id { get; set; }
        public int TransID { get; set; }
        public string? AgentName { get; set; }
        public string? CEANo { get; set; }
        public double? GrossValue { get; set; }
        public double? NetValue { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }
        public string? ProjectNane { get; set; }
        public double? TransactedPrice { get; set; }
        public double? TransactedCol { get; set; }
        [EpplusIgnore]
        public List<TransactionGcedata> Gcedata { get; set; }
        [EpplusIgnore]
        public List<TransactionGcrdata> Gcrdata { get; set; }
    }
}
