using AcmeOrder.Web.Models;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeOrder.Web.Service
{
    public interface IProductService {
        Task<Product> GetProduct(string productCode);
        Task ProcessOrder(OrderViewModel order);
    }


    public class ProductService:IProductService
    {
        private string baseUrl = "https://localhost:16335";

        public async Task<Product> GetProduct(string productCode)
        {
        ///https://localhost:16335/api/product/AHC-456

            var product = await baseUrl
                .AppendPathSegments("api", "product", productCode)
                .GetJsonAsync<Product>();
            return product;
        }

        public async Task ProcessOrder(OrderViewModel order)
        {

            var person = await baseUrl
                 .AppendPathSegments("api", "order")

                .PostJsonAsync(order)
                .ReceiveJson();
        }
    }
}
