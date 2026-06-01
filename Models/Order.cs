using System;
using System.Collections.Generic;

namespace demka_podg1.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public int? PickupPointId { get; set; }

    public int? UserId { get; set; }

    public int? Code { get; set; }

    public int? StatusId { get; set; }

    public virtual PickupPoint? PickupPoint { get; set; }

    public virtual OrderStatus? Status { get; set; }

    public virtual User? User { get; set; }
}
