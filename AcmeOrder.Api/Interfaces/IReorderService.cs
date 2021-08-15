using System;
using System.Threading.Tasks;

namespace AcmeOrder.Api.Interfaces
{
    public interface IReorderService
    {
        Task Order(string productCode, int reorderQuantity);
    }
}
