using Ecommerce12.DAL.DTO_s.Request.Cart;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.CartResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  interface ICartService
    {

        Task<BaseResponse> AddToCart(string userId, AddToCartRequest request);

        Task<CartSummaryResponse> getCartForUser(string userId, string Language = "en");

        Task<BaseResponse> UpdateQuantity(int productId, string userId, int count);
        Task<BaseResponse> ClearCart(string userId);

        Task<BaseResponse> RemoveItemFromCart(string userId, int productId);
    }
}
