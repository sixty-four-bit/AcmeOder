using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeOrder.Web.Models;
using AcmeOrder.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeOrder.Web.Controllers
{
    public class OrderController : Controller
    {

        IProductService _productService;

        public OrderController(IProductService productService) {
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult Index()
        {

            var order = new OrderViewModel();
            return View(order);
        }
        
        [HttpPost]
        public async Task<ActionResult> Index(OrderViewModel model, string formAction)
        {
            var order = new OrderViewModel();

            if (formAction.Equals("Submit Order"))
            {
                await _productService.ProcessOrder(model);
                ViewData["errormessage"] = "Order submitted";
            }
            else
            {
                order.ProductLineItems = model.ProductLineItems;
                var product = await _productService.GetProduct(model.SearchProduct);
                if (product == default(Product))
                    ViewData["errormessage"] = "Product not found";
                else
                {
                    model.SearchProduct = string.Empty;
                    order.ProductLineItems.Add(new ProductLineItemViewModel
                    {
                        ProductCode = product.ProductCode,
                        Description = product.Description,
                        Quantity = 1,
                        UnitPrice = product.UnitPrice
                    });
                }
            }
            return View(order);
        }
        
    }
}