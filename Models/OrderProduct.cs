using System;
using System.Collections.Generic;

namespace demka_podg1.Models;

public partial class OrderProduct
{
    public int? OrderId { get; set; }

    public string? ProductId { get; set; }

    public int? Count { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
