using System;
using System.Collections.Generic;

namespace AcmeOrder.Api.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
        }

        public List<ProductLineItemViewModel> ProductLineItems { get; set; }

    }
}
