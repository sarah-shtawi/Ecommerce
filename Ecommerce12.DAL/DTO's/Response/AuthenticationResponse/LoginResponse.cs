using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse
{
    public  class LoginResponse : BaseResponse
    {
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
