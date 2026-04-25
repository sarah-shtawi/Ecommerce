using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public  interface IReviewRepository
    {

        Task<bool> HasUserReviewedProduct(string userId, int productId);

        Task<Review> CreateReview(Review Request);

    }
}
