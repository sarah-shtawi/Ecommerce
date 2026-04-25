using Ecommerce12.DAL.DTO_s.Request.Reviews;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository , IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<BaseResponse> AddReview(CreateReviewRequest request , string userId , int productId)
        {
            // اذا الطلب وصل او لا 
            var HasProductDeliverd = await _orderRepository.HasUserDeliverdOrderForProduct(userId , productId); // true 
            if (!HasProductDeliverd) 
            {
               return new BaseResponse
                 {
                    Success = false ,
                    Message = "you can't review Product you have received"
                 };
            }
            // اذا المستخدم علق ام لا على المنتج 
            var HasUserReviewedProduct = await _reviewRepository.HasUserReviewedProduct(userId , productId); // no false
            if (HasUserReviewedProduct)
            {
                return new BaseResponse
                {
                    Success= false ,
                    Message = "can't add Review"
                };
            }

            var review = request.Adapt<Review>();
            review.UserId = userId;
            review.ProductId = productId;
            await _reviewRepository.CreateReview(review);

            return new BaseResponse
            {
                Success = true ,
                Message = "review added successfully"
            };
        }
    }
}
