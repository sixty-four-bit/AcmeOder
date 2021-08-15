using System;
using System.Threading.Tasks;
using AcmeOrder.Api.ViewModels;

namespace AcmeOrder.Api.Interfaces
{
    public interface IOrderService
    {
        Task ProcessOrder(OrderViewModel model);
    }
}
