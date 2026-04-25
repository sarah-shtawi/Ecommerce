using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public  interface ICartRepository
    {

        Task<Cart> AddToCart(Cart Request);

        Task<List<Cart>> getCartForUser(string userId);

        Task<Cart?> findProductInUserCart(string userId, int productId);

        Task<Cart> UpdateAsync(Cart cart);

        Task ClearCart(string userId);
        Task DeleteProductFromCart(Cart cart);
    }
}
