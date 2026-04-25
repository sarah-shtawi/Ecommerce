using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.User
{
    public  class UserListResponse
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public bool IsBlocked { get; set; }

        public List<string> Roles { get; set; }
    }
}
