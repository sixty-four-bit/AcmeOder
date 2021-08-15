using System;
using System.Linq;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Repository;

namespace AcmeOrder.Api.Services
{
    public class ReorderService:IReorderService
    {
        IFileRepository _fileRepository;
        public ReorderService(IFileRepository fileRepository) {
            _fileRepository = fileRepository;
        }

      
        public async Task Order(string productCode, int reorderQuantity)
        {
            var reorderedProducts = await _fileRepository.GetReorderProducts();
            var isReordered = reorderedProducts?.Any(p => p.ProductCode.Equals(productCode))??false;
            if (isReordered) return;

            reorderedProducts.Add(new Models.ReorderProduct
            {
                ProductCode = productCode,
                ReorderQuantity = reorderQuantity
            });

            await _fileRepository.WriteReorderProducts(reorderedProducts);
        }

        
    }
}