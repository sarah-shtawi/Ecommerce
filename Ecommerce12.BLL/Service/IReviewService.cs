using Ecommerce12.DAL.DTO_s.Request.Reviews;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public interface IReviewService
    {
        Task<BaseResponse> AddReview(CreateReviewRequest request, string userId, int productId);
    }
}
