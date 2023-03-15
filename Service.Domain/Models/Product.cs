using System;
using System.Collections.Generic;

namespace Service.Domain.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public double? Price { get; set; }
}
