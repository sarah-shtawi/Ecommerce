using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.DTO_s.Request.Category;
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
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _CatergoryService;
        private readonly IStringLocalizer _localizer;
        public CategoriesController(ICategoryService CatergoryService , IStringLocalizer<SharedResource> localizer)
        {
            _CatergoryService = CatergoryService;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _CatergoryService.GetAllCategories();
            return Ok(new { message = _localizer["Success"].Value, categories });
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var category = await _CatergoryService.CreateCategory(request);
            return Ok(new { message = _localizer["Success"].Value } );
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id , [FromBody] CategoryRequest category )
        {
            var result = await _CatergoryService.UpdateCategoryAsync(id, category);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Ok(result);
        }

        [HttpPatch("toggle-status/{id}")]
        public async Task <IActionResult> ToggleStatus(int id )
        {
            var result = await _CatergoryService.ToggleStatus(id);
            if (!result.Success)
            {
                if (result.Message.Contains("Not Found"))
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var result = await _CatergoryService.DeleteAsync(id);
            if (!result.Success)
            { 
              if(result.Message.Contains("Not Found"))
               {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Ok(result);
        
        }
    }
}
