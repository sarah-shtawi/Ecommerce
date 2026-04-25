using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public IQueryable <Product> Query ()
        {
            return _context.products.Include(c => c.Translations).AsQueryable();
        }

        public async Task<Product?> FindByIdAsync(int id)
        {
            var Product = await _context.products.Include(c => c.Translations)
                .Include(c=>c.SubImages)
                .Include(c=>c.Reviews)
                .ThenInclude(r=>r.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            return Product;
        }

        public async Task <bool> DecreaseQuantitiesAsync( List<(int productId, int quantity)> items )
        {
            var productIds = items.Select(p => p.productId).ToList();// Ids in items 
            var products = await _context.products.Where(p => productIds.Contains(p.Id)).ToListAsync(); // products update qty 
            foreach (var product in products)
            {
                var item = items.FirstOrDefault( p=>p.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    return false;
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
