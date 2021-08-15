using System;
namespace AcmeOrder.Api.ViewModels
{
    public class ProductDto
    {
        public ProductDto()
        {

        }

        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
