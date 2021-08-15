using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeOrder.Api.Interfaces;
using AcmeOrder.Api.Services;
using AcmeOrder.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] OrderViewModel  model)
        {
            try
            {
                await _orderService.ProcessOrder(model);
                return Created("",new { });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       
    }
}
