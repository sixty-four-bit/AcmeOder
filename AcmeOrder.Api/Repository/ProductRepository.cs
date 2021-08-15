using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Models;

namespace AcmeOrder.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _productList;
        private IFileRepository _fileRepository;

        public  ProductRepository(IFileRepository fileRepository)
        {
            _productList = new List<Product>();
            _fileRepository = fileRepository;
        }

        private async Task PopulateProduct()
        {
            var products = await _fileRepository.GetProducts();
            _productList=products;
        }


        public async Task<Product> GetProduct(string productId)
        {
            await PopulateProduct();
            var product = _productList.FirstOrDefault(p => p.ProductCode.Equals(productId));
            return product;
        }

        public async Task WriteProduct(Product product)
        {
            await PopulateProduct();
            var dbProduct = _productList.FirstOrDefault(p => p.ProductCode.Equals(product.ProductCode));
            dbProduct.OnOrder=product.OnOrder;
            dbProduct.StockLevel = product.StockLevel;
            
            await _fileRepository.WriteProducts(_productList);
            return;
        }

    }
}
