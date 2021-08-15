namespace AcmeOrder.Api.ViewModels
{
    public class ProductLineItemViewModel
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}