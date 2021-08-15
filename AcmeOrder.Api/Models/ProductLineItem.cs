using System;
namespace AcmeOrder.Api.Models
{
    public class ProductLineItem
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total => UnitPrice * Quantity;


        public ProductLineItem()
        {

        }

        public string GetString() {
            return $"{ProductCode},{Description}, {UnitPrice}, {Quantity}, {Total}";
        }
    }
}
