using System;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Models;
using AcmeOrder.Api.Repository;
using AcmeOrder.Api.ViewModels;

namespace AcmeOrder.Api.Services
{
    


    public class OrderService:IOrderService
    {

        IProductRepository _productRespository;
        IReorderService _reorderService;
        IFileRepository _fileRepository;

        public OrderService(IProductRepository productRepository, IReorderService reorderService, IFileRepository fileRepository)
        {
            _productRespository = productRepository;
            _reorderService = reorderService;
            _fileRepository = fileRepository;
        }

        public async Task ProcessOrder(OrderViewModel model)
        {

            var order = new Order();

            if (!await IsOrderFullfillable(model))
                throw new Exception("Order quantity not valid");

            foreach (ProductLineItemViewModel productLineItem in model.ProductLineItems)
            {

                var product = await _productRespository.GetProduct(productLineItem.ProductCode);

                if (product.StockLevel >= productLineItem.Quantity)
                {
                    await ReduceProductStock(product, productLineItem.Quantity);

                    if (product.IsReorder)
                    {
                        await ReorderProduct(product);
                    }
                }
                else
                {
                    productLineItem.Quantity = 0;  
                }

                order.ProductLineItems.Add(new ProductLineItem
                {
                    ProductCode = productLineItem.ProductCode,
                    Description = productLineItem.Description,
                    Quantity = productLineItem.Quantity,
                    UnitPrice= productLineItem.UnitPrice
                });

            }
                      
            await _fileRepository.WirteToOrderFile(order);

        }


        private async Task<bool> IsOrderFullfillable(OrderViewModel model) {
            foreach (ProductLineItemViewModel productLineItem in model.ProductLineItems)
            {
                var product = await _productRespository.GetProduct(productLineItem.ProductCode);
                if (product.StockLevel < productLineItem.Quantity)
                    return false;
             }

            return true;
        }

        private async Task ReorderProduct(Product product)
        {

           await _reorderService.Order(product.ProductCode, product.ReorderQuantity);
        }


        public async Task ReduceProductStock(Product product,int quantity)
        {
            product.AddOnOrder(quantity);
            await _productRespository.WriteProduct(product);
        }

    }
}
