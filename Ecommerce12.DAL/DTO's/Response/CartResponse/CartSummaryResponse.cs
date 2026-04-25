using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.CartResponse
{
    public  class CartSummaryResponse
    {

        public List<AddToCartResponse> items {  get; set; }

        public decimal CartTotal => items.Sum(i => i.TotalPrice);



    }
}
