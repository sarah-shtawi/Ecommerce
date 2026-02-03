using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.CategoryResponse
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public Status status { get; set; }
        public List <CategoryTrnslationsResponse> Translation {  get; set; }
       
    }
}
