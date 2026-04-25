using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Product
{
    public class ProductUserDetailsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }

        public List<string> SubImages { get; set; }
        public List<ReviewsResponse> Reviews { get; set; }

    }
}
