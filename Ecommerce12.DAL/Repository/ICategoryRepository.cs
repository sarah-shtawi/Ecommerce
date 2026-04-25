using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public interface ICategoryRepository
    {

        public Task<List<Category>> GetAll();

        public Task<Category> CreateCategoryRepo(Category Request);

        public Task<Category?> FindByIdAsync(int id);

        public Task DeleteCategoryAsync(Category category);

        public Task<Category?> UpdateCategoryAsync(Category request);

    }
}
