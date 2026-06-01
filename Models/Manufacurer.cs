using System;
using System.Collections.Generic;

namespace demka_podg1.Models;

public partial class Manufacurer
{
    public int Id { get; set; }

    public string Manufacurer1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
