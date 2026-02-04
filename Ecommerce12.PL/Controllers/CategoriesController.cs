using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Ecommerce12.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce12.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer _localizer;

        public CategoriesController(ICategoryService categoryService, IStringLocalizer <SharedResource> localizer) 
        {
            _categoryService = categoryService;
            _localizer = localizer;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            var Categories = _categoryService.GetAllCategories();
            return Ok( new { message = _localizer["Success"].Value , Categories } );
        }


        [HttpPost("")]
        public IActionResult CreateCategory (CategoryRequest request)
        {
            var categoryRes = _categoryService.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value  });
        }

    }
}
