using System;
using System.Collections.Generic;
using System.Linq;

namespace AcmeOrder.Api.Models
{
    public class Order
    {
        public Order()
        {
            ProductLineItems = new List<ProductLineItem>();

        }

        public List<ProductLineItem> ProductLineItems { get; set; }
        public decimal OrderTotal => ProductLineItems.Select(p => p.Total).Sum();
    }
}
