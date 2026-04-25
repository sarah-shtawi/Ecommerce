using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.CategoryResponse
{
    public class CategoryResponse
    {
        public int Id { get; set; }

       // public ApplicationUser User { get; set; }
        public string CreatedBy { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status status { get; set; }
        public List <CategoryTrnslationsResponse> Translation {  get; set; }
    }
}
