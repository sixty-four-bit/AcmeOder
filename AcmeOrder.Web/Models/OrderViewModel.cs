using System;
using System.Collections.Generic;

namespace AcmeOrder.Web.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            ProductLineItems = new List<ProductLineItemViewModel>();
        }
        public List<ProductLineItemViewModel> ProductLineItems { get; set; }
        public string SearchProduct { get; set; }
    }

    public class ProductLineItemViewModel
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }
}
