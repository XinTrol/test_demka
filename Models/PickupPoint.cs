using System;
using System.Collections.Generic;

namespace demka_podg1.Models;

public partial class PickupPoint
{
    public int Id { get; set; }

    public int? Postcode { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? House { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
