using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.CheckOut
{
    public  class CheckOutResponse : BaseResponse
    {
        public string? URL { get; set; }
        public string? PaymentId { get; set; }
    }
}
