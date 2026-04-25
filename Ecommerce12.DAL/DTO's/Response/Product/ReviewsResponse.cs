using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Product
{
    public  class ReviewsResponse
    {
        public string Name { get; set; }

        public int Rating { get; set; } 
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
