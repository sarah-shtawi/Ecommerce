using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Category CreateCategoryRepo(Category Request)
        {
            _context.categories.Add(Request);
            _context.SaveChanges();
            return Request;
        }

        public List<Category> GetAll()
        {
            return _context.categories.Include(c =>c.Translation).ToList();
        }

    }
}
