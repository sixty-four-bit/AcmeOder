using System;
using System.Threading.Tasks;
using AcmeOrder.Api.ViewModels;

namespace AcmeOrder.Api.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProduct(string productId);
    }
}
