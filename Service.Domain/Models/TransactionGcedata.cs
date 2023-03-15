using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Service.Domain.Models;

public partial class TransactionGcedata
{
    public long Id { get; set; }
    public DateTime KeyinDate { get; set; }

    public DateTime SubDate { get; set; }

    public DateTime OptionDate { get; set; }

    public int TypeId { get; set; }

    public string? RoleDisplay { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryDisplay { get; set; }

    public string? AgtCeano { get; set; }

    public string? AgtBizName { get; set; }

    public double GrossValue { get; set; }

    public double NetValue { get; set; }

    public double StatisticsRatio { get; set; }

    public int TransId { get; set; }
    public TransactionGcedata transactionGcedata { get; set; }
}
