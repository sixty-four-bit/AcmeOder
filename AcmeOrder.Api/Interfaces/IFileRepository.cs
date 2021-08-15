using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeOrder.Api.Models;

namespace AcmeOrder.Api.Interfaces
{
    public interface IFileRepository
    {
        Task WirteToOrderFile(Order order);
      
       
        Task<List<Product>> GetProducts();
        Task<List<ReorderProduct>> GetReorderProducts();
        Task WriteReorderProducts(List<ReorderProduct> products);
        Task WriteProducts(List<Product> productList);
    }
}
