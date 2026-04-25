using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.CartResponse
{
    public  class AddToCartResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; } 

        public decimal Price { get; set; }

        public decimal TotalPrice => Count * Price;


    }
}
