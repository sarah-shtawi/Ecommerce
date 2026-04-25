using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.AuthenticationRequest
{
    public  class TokenApiModel
    {

        public string? accessToken { get; set; }

        public string? refreshToken { get; set; }
    }
}
