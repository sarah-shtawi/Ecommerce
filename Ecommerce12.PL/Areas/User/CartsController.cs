using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.Cart;
using Ecommerce12.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> GetItemsOfCart([FromQuery] string Language = "en")
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.getCartForUser(userId, Language);
            return Ok(cart);

        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCart(userId, request);
            return Ok(result);
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute] int productId , [FromBody] UpdateQuantityRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.UpdateQuantity(productId, userId, request.count);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.ClearCart(userId);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task <IActionResult> RemoveProductFromCart([FromRoute] int productId )
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.RemoveItemFromCart(userId, productId);
            return Ok(result);
        }


    }
}
