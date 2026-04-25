using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Product
{
    public class ProductResponse
    {

        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status status { get; set; }

        public string CreatedBy { get; set; }
        public string MainImage { get; set; }
        public int Quantity { get; set; }
        public List<ProductTranslationResponse> Translations { get; set; }

    }
}
