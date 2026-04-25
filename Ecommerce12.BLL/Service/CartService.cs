using Ecommerce12.DAL.DTO_s.Request.Cart;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.CartResponse;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductRepository productRepository , ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<BaseResponse> AddToCart(string userId , AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);

            if (product == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product Not Found"
                };
            }
            // product we want to add 
            var cartItem = await _cartRepository.findProductInUserCart(userId, request.ProductId);

            // count of product in cart 
            var ExistingCount = cartItem?.Count ?? 0; 

           
            if (product.Quantity < (request.Count + ExistingCount ))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not enough stock"
                };
            }
      
            if(cartItem is not null)
            {
                cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.UserId = userId;
                await _cartRepository.AddToCart(cart);
            }
            return new BaseResponse
            {
                Success = true,
                Message = "Product added successfully"
            };
        }

        public async Task <CartSummaryResponse> getCartForUser(string userId , string Language = "en")
        {
            var cart = await _cartRepository.getCartForUser(userId);

            var items = cart.Select(c => new AddToCartResponse
            {
                ProductId = c.ProductId,
                ProductName = c.Product.Translations.FirstOrDefault(t=> t.Language == Language).Name,
                Count = c.Count,
                Price = c.Product.Price
            }).ToList();

            return new CartSummaryResponse
            {
                items = items,
            };
        }

        public async Task<BaseResponse> UpdateQuantity(int productId , string userId ,int count )
        {
            var cartItem = await _cartRepository.findProductInUserCart(userId , productId);
            var product = await _productRepository.FindByIdAsync(productId);
            if(count == 0)
            {
                await _cartRepository.DeleteProductFromCart(cartItem);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Product deleted Successfully"
                };
            }
            if (count < 0)
            {
                return new BaseResponse
                {
                    Success= false , 
                    Message = "invalied count"
                };
            }
            if (product.Quantity < count) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "not enough stock"
                };
            }
            cartItem.Count = count;
            await _cartRepository.UpdateAsync(cartItem);
            return new BaseResponse
            {
                Success = true,
                Message = "Quantity Updated Sucessfully"
            };
        }
        public async Task<BaseResponse> ClearCart(string userId)
        {
           await _cartRepository.ClearCart(userId);
            return new BaseResponse
            {
                Success= true,
                Message = "Cart cleard Successfully"
            };
        }

        public async Task<BaseResponse> RemoveItemFromCart(string userId , int productId)
        {
            var cartItem = await _cartRepository.findProductInUserCart(userId , productId);
            if(cartItem is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product Not Found"
                };
            }
            await _cartRepository.DeleteProductFromCart(cartItem);
            return new BaseResponse
            {
                Success = true,
                Message = "Product Deleted Successfully"
            };
        }

    }
}
