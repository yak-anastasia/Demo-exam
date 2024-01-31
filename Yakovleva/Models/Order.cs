﻿using System;
using System.Collections.Generic;

namespace Yakovleva.Models;

public partial class Order
{
    public int Id { get; set; }

    public byte[] Date { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int UserId { get; set; }

    public string Place { get; set; } = null!;

    public int CountPerson { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User User { get; set; } = null!;
}
