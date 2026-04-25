using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.Cart
{
    public class AddToCartRequest
    {

        public int ProductId { get; set; }

        public int Count { get; set; }

    }
}
