using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using Ecommerce12.DAL.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer _localizer;

        public CategoriesController(ApplicationDbContext context , IStringLocalizer <SharedResource> localizer) 
        {
            _context = context;
            _localizer = localizer;
        }

        [HttpGet("")]

        public IActionResult Index()
        {
            var categoriesDB = _context.categories.Include(c => c.Translation).ToList();
            var categories = categoriesDB.Adapt <List<CategoryResponse>>();
            return Ok( new { message = _localizer["Success"].Value , categories } );
        }

        [HttpPost("")]
        public IActionResult CreateCategory (CategoryRequest request)
        {
            var categoryDB = request.Adapt<Category>();
            _context.categories.Add(categoryDB);
            _context.SaveChanges();
            return Ok(new { message = _localizer["Success"].Value });
        }

    }
}
