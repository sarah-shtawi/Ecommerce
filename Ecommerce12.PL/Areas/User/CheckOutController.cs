using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.CheckOut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Globalization;
using System.Security.Claims;

namespace Ecommerce12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;

        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }

        [HttpPost]
         public async Task<IActionResult> ProcessPaymentAsync(CheckOutRequest request)
         {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var PaymentURL = await _checkOutService.ProcessPaymentAsync(request , userId);
            if (userId is null) 
            {
                return BadRequest();
            }
            return Ok(PaymentURL);
         }

        [HttpGet("success")]
        public async Task<IActionResult> Success([FromQuery] string session_id)
        {
            var response = await _checkOutService.HandleSuccessAsync(session_id);
            if (!response.Success)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
