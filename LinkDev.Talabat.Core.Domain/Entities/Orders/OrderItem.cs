﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class OrderItem : BaseAuditableEntity<int>
    {
        public virtual ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
