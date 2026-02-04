using Ecommerce12.DAL.DTO_s.Request.Category;
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
        public List<CategoryResponse> GetAllCategories();

        public CategoryResponse CreateCategory(CategoryRequest Request);

    }

}
