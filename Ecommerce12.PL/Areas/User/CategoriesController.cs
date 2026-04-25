using Ecommerce12.BLL.Service;
using Ecommerce12.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Ecommerce12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer _localizer;

        public CategoriesController(ICategoryService categoryService , IStringLocalizer<SharedResource> localizer ) 
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task< IActionResult> IndexForUser(string Lang = "en")
        {
            var categories = await _categoryService.GetAllCategoriesForUser(Lang);
            return Ok (new { message = _localizer["Success"].Value , categories });

        }
    }
}
