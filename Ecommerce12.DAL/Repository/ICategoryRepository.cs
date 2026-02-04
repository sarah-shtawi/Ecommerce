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

        public List<Category> GetAll();

        public Category CreateCategoryRepo(Category Request);

    }
}
