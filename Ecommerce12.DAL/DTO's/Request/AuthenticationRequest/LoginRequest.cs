using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.AuthenticationRequest
{
    public  class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
