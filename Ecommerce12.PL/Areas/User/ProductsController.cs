using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.Reviews;
using Ecommerce12.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace Ecommerce12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IReviewService _reviewService;

        public ProductsController( IProductService productService , IStringLocalizer <SharedResource> localizer , IReviewService reviewService  )
        {
            _productService = productService;
            _localizer = localizer;
            _reviewService = reviewService;
        }

        [HttpGet("")]
        public async Task<IActionResult> IndexForUser([FromQuery] string Language = "en" , [FromQuery] int page = 1 ,
            [FromQuery] int limit = 3 , string? search = null , [FromQuery] int? categoryId = null,
            [FromQuery] decimal? MinPrice = null, [FromQuery] decimal? MaxPrice = null , [FromQuery] string? sortBy = null, [FromQuery] bool asc = true)
        {
            var products = await _productService.GetAllProductsForUser(Language , page , limit , search , categoryId, MinPrice, MaxPrice, sortBy, asc);
            return Ok(new { message = _localizer["Success"].Value, products });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails([FromRoute ]int id , [FromQuery] string Language = "en")
        {
            var product = await _productService.GetProductDetails(id, Language);
            return Ok(new { message = _localizer["Success"].Value  , product});
        }


        [HttpPost("{productId}/reviews")]
        public async Task<IActionResult> addReview([FromRoute] int productId , [FromBody] CreateReviewRequest request )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _reviewService.AddReview(request ,userId, productId);
            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }
            return Ok(new { message = response.Message });

        }
    }
}
