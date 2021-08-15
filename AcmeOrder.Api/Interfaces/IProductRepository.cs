using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeOrder.Api.Models;

namespace AcmeOrder.Api.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(string productId);
        Task WriteProduct(Product product);
    }
}
