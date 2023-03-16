using System;
using System.Collections.Generic;

namespace Service.Domain.Models;

public partial class TransactionGcrdata
{
    public long Id { get; set; }

    public string? TransSource { get; set; }

    public DateTime RcvDate { get; set; }

    public int TypeId { get; set; }

    public string? RoleDisplay { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryDisplay { get; set; }

    public string? AgtCeano { get; set; }

    public string? AgtBizName { get; set; }

    public double GrossValue { get; set; }

    public double NetValue { get; set; }
    public int TransId { get; set; }
    public virtual TransactionData TransactionData { get; set; }

}
