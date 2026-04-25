using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Product
{
    public class ProductsResponseUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public int Quantity { get; set; }

    }
}
