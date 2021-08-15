using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Models;
using AcmeOrder.Api.ViewModels;
using CsvHelper;
using CsvHelper.Configuration;

namespace AcmeOrder.Api.Repository
{
    public class FileProcessor : IFileRepository
    {
        public FileProcessor()
        {

        }
        string productUrl = "Z:\\Projects\\AcmeOrder\\AcmeOrder.Api\\AcmeHardwareData.csv";
        string reorderProductUrl = "reorder.csv";

        public async Task WirteToOrderFile(Order order)
        {
            using (StreamWriter file = new StreamWriter("Order.txt",true))
            {
                await file.WriteLineAsync("New Order ------------------------------");
                foreach (ProductLineItem productLineItem in order.ProductLineItems)
                {
                    await file.WriteLineAsync(productLineItem.GetString());
                }
                await file.WriteLineAsync($"Order Total {order.OrderTotal}");
            };

        }


        public async Task<List<Product>> GetProducts() {
            
            using (var reader = new StreamReader(productUrl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Product>().ToList();
                return records;
            }
        }

        public async Task WriteProducts(List<Product> products)
        {
            using (var reader = new StreamWriter(productUrl))
            using (var csv = new CsvWriter(reader, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(products);
            }
        }

        public async Task<List<ReorderProduct>> GetReorderProducts()
        {

            using (var reader = new StreamReader(reorderProductUrl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ReorderProduct>().ToList();
                return records;
            }
        }

        public async Task WriteReorderProducts(List<ReorderProduct> products)
        {
            using (var reader = new StreamWriter(reorderProductUrl))
            using (var csv = new CsvWriter(reader, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(products);
            }
        }
    }
}
