using Ecommerce12.DAL.DTO_s.Request.CheckOut;
using Ecommerce12.DAL.DTO_s.Response.CheckOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  interface ICheckOutService
    {
        Task <CheckOutResponse> ProcessPaymentAsync (CheckOutRequest request, string userId);

        Task<CheckOutResponse> HandleSuccessAsync(string sessionId);

    }
}
