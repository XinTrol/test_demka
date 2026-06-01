using System;
using System.Collections.Generic;

namespace demka_podg1.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Supplier1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
