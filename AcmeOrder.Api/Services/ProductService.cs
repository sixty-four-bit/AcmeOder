using System;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Repository;
using AcmeOrder.Api.ViewModels;

namespace AcmeOrder.Api.Services
{
    public class ProductService:IProductService
    {
        IProductRepository _productRespository;

        public ProductService(IProductRepository productRepository)
        {
            _productRespository = productRepository;
        }


        public async Task<ProductDto> GetProduct(string productId) {

            var product = await  _productRespository.GetProduct(productId);

            return new ProductDto
            {
                 ProductCode= product.ProductCode,
                 Description= product.Description,
                 UnitPrice= product.UnitPrice
            };
        }
    }
}
