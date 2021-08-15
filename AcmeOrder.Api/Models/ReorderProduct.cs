using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeOrder.Api.Models
{
    public class ReorderProduct
    {
        public string ProductCode { get; set; }
        public int ReorderQuantity { get; set; }
    }
}
