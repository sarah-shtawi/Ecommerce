using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public CategoryResponse CreateCategory(CategoryRequest Request)
        {
            var CategoryReq = Request.Adapt<Category>();
            var categoryDB =  _categoryRepository.CreateCategoryRepo(CategoryReq);
            var categoryRes = categoryDB.Adapt<CategoryResponse>();
            return categoryRes;
        }



        public List<CategoryResponse> GetAllCategories()
        {
            var CategoriesDB = _categoryRepository.GetAll();
            var categoriesRes = CategoriesDB.Adapt<List<CategoryResponse>>();
            return categoriesRes;
        }
    }
}
