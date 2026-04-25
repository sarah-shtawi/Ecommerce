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
        public async Task<Category> CreateCategoryRepo(Category Request)
        {
           await  _context.categories.AddAsync(Request);
           await  _context.SaveChangesAsync();
            return Request;
        }
        public async Task<List<Category>> GetAll()
        {
            return await  _context.categories.Include(c =>c.Translation).Include(c =>c.User).ToListAsync();
        }
        public async Task<Category?> FindByIdAsync(int id )
        {
            var category = await _context.categories.Include(c => c.Translation).FirstOrDefaultAsync(c=> c.Id == id);
            return category;
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            _context.categories.Remove(category);
            _context.SaveChangesAsync();
        }
        public async Task<Category?> UpdateCategoryAsync(Category request)
        {
            _context.categories.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
