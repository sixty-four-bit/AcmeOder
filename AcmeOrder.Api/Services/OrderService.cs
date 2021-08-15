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
                    //What happens with the order if requested quantity is not availbale depends on business 
                    //For now just using 0 quanity 
                    //We may inform user imeediately or we can expect it will be available later with reorder
                    //Or we my inform during delivery. 
                    //As there is no clear deatails in question. just doing this
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
