using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.AuthenticationRequest
{
    public class ResetPasswordRequest
    {
        public string code { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}
