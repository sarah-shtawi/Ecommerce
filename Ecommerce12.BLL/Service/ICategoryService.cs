using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponse>> GetAllCategories();

        public Task<List<CategoryResponseForUser>> GetAllCategoriesForUser(string Language = "en");
        public Task<CategoryResponse> CreateCategory(CategoryRequest Request);

        public Task<BaseResponse> DeleteAsync(int id);

        public  Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
        public Task<BaseResponse> ToggleStatus(int id);

    }

}
