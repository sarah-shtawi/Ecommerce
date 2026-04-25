using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.Order;
using Ecommerce12.DAL.Models;
using Ecommerce12.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Ecommerce12.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public OrdersController(IOrderService orderService , IStringLocalizer<SharedResource> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders( [FromQuery] OrderStatusEnum status = OrderStatusEnum.Pending) 
        {
            var orders = await _orderService.GetOrders(status);
            return Ok(orders);
          
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int orderId , [FromBody] UpdateOrderRequest request )
        {
            var result = await _orderService.UpdateOrderStatus(orderId , request.Status);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok (result);
        }
    }
}
