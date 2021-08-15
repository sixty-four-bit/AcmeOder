using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Models;
using AcmeOrder.Api.Repository;
using System.Threading.Tasks;
using FluentAssertions;
using AcmeOrder.Api.Services;
using AcmeOrder.Api.ViewModels;
using System.Linq;

namespace AcmeOrder.Api.Tests
{
    public class OrderTests
    {
        List<Product> products = new List<Product>();
        OrderViewModel order = new OrderViewModel();
        public OrderTests()
        {
            products.Add(new Product
            {
                ProductCode = "P1",
                Description = "Product one",
                OnOrder = 0,
                ReorderLevel = 10,
                ReorderQuantity = 20,
                StockLevel = 20,
                UnitPrice = 5.0m
            });

            products.Add(new Product
            {
                ProductCode = "P2",
                Description = "Product two",
                OnOrder = 0,
                ReorderLevel = 10,
                ReorderQuantity = 20,
                StockLevel = 20,
                UnitPrice = 5.0m
            });

            order = new OrderViewModel
            {
                ProductLineItems = new List<ProductLineItemViewModel> {
                      new ProductLineItemViewModel{
                          ProductCode="P1",
                          Description="",
                          Quantity=3,
                          UnitPrice=5
                      }
             }
            };
        }

        [Fact]
        public async Task Product_Get_Success()
        {
            var fileProcess = Substitute.For<IFileRepository>();
            fileProcess.GetProducts().Returns(products);
            var productRepository = new ProductRepository(fileProcess);

            var retrivedProducts = await productRepository.GetProduct("P1");

            retrivedProducts.ProductCode.Should().Be("P1");
        }

        [Fact]
        public async Task Order_Success()
        {
            var fileProcess = Substitute.For<IFileRepository>();
            fileProcess.GetProducts().Returns(products);
            var productRepository = new ProductRepository(fileProcess);
            var reorderServcie = new ReorderService(fileProcess);
            var orderService = new OrderService(productRepository, reorderServcie, fileProcess);

            await orderService.ProcessOrder(order);
            var retrivedProducts = await productRepository.GetProduct("P1");

            retrivedProducts.StockLevel.Should().Be(17);
            retrivedProducts.OnOrder.Should().Be(3);

        }

        [Fact]
        public async Task Order_with_reorder_Success()
        {
            OrderViewModel order = new OrderViewModel();
            order = new OrderViewModel
            {
                ProductLineItems = new List<ProductLineItemViewModel> {
                      new ProductLineItemViewModel{
                          ProductCode="P2",
                          Description="",
                          Quantity=10,
                          UnitPrice=5
                      }
             }
            };
            var fileProcess = Substitute.For<IFileRepository>();
            fileProcess.GetProducts().Returns(products);
            fileProcess.GetReorderProducts().Returns(new List<ReorderProduct>() );
            var productRepository = new ProductRepository(fileProcess);
            var reorderServcie = new ReorderService(fileProcess);
            var orderService = new OrderService(productRepository, reorderServcie, fileProcess);
           

            await orderService.ProcessOrder(order);
            var retrivedProducts = await productRepository.GetProduct("P2");
            var reorderedProducts = await fileProcess.GetReorderProducts();
            var p2Product = reorderedProducts.FirstOrDefault(p=>p.ProductCode.Equals("P2"));

            retrivedProducts.StockLevel.Should().Be(10);
            retrivedProducts.OnOrder.Should().Be(10);
            p2Product.Should().NotBeNull();
            p2Product.ProductCode.Should().Equals("P1");
            p2Product.ReorderQuantity.Should().Be(20);

        }
    }
}
