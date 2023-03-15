using System;
using System.Collections.Generic;

namespace Service.Domain.Models;

public partial class TransactionData
{
    public TransactionData()
    {
        TransactionGcedata = new HashSet<TransactionGcedata>();
        TransactionGcrdatas = new HashSet<TransactionGcrdata>();
    }
    public int TransId { get; set; }

    public int TransTypeId { get; set; }

    public string? TransType { get; set; }

    public string? PropertyType { get; set; }

    public int? PropertyCategoryId { get; set; }

    public string? PropertyCategory { get; set; }

    public string? PropertyModel { get; set; }

    public DateTime KeyinDate { get; set; }

    public DateTime SubDate { get; set; }

    public string? SalesType { get; set; }

    public string? ClosingAgtCeano { get; set; }

    public string? ClosingAgtBizName { get; set; }

    public DateTime OptionDate { get; set; }

    public string? HseNo { get; set; }

    public string? StreetName { get; set; }

    public string? ProjectName { get; set; }

    public double TransactedPrice { get; set; }

    public double TransactionComm { get; set; }

    public string? CurrencyType { get; set; }

    public string? CultureCode { get; set; }

    public DateTime GeneratedDate { get; set; }

    public string? LastUpdater { get; set; }

    public ICollection<TransactionGcedata> TransactionGcedata { get; set; }
    public ICollection<TransactionGcrdata> TransactionGcrdatas { get; set; }
}
