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
    public  class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewedProduct(string userId , int productId )
        {
            return await _context.Reviews.AnyAsync(r=>r.UserId == userId && r.ProductId == productId);
        }

        public async Task <Review> CreateReview(Review Request)
        {
            await _context.Reviews.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

    }
}




