using Azure.Core;
using Ecommerce12.DAL.Data;
using Ecommerce12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public  class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task <Cart> AddToCart( Cart Request)
        {
            await _context.carts.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

        public async Task <List<Cart>> getCartForUser(string userId)
        {
            var cart = await _context.carts.Where(c=>c.UserId == userId).Include( c=>c.Product.Translations).ToListAsync();
            return cart;
        }

        public async Task<Cart?> findProductInUserCart(string userId , int productId)
        {
            return await _context.carts.FirstOrDefaultAsync(c=>c.UserId == userId && c.ProductId== productId);
        }
        public async Task<Cart> UpdateAsync(Cart cart)
        {
            _context.carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task ClearCart(string userId)
        {
            var cart =  await _context.carts.Where(c =>c.UserId == userId).ToListAsync();
            _context.RemoveRange(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductFromCart(Cart cart)
        {
          _context.carts.Remove(cart);
          await _context.SaveChangesAsync();
        }
    }
}
