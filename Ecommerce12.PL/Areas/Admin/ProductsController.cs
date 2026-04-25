using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.Product;
using Ecommerce12.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Ecommerce12.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles=("Admin"))]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer _localizer;

        public ProductsController(IProductService productService ,IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            var product = await _productService.CreateProduct(request);
            return Ok(new { message = _localizer["Success"].Value , product });

        }

        [HttpGet("")]
        public async Task<IActionResult> IndexProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(new { message = _localizer["Success"].Value, products });

        }




    }
}
