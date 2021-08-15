using System;
namespace AcmeOrder.Api.Models
{
    public class Product
    {
        public Product()
        {
        }


        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int StockLevel { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public int OnOrder { get; set; }
        public bool IsReorder => (ReorderLevel >= StockLevel);
        public decimal UnitPrice { get;  set; }


        public void AddOnOrder(int quantity) {
            this.StockLevel -= quantity;
            this.OnOrder += quantity;
        }
    }


}


