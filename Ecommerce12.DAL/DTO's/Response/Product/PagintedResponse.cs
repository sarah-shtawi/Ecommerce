using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Response.Product
{
    public  class PagintedResponse<T> 
    {
        public int TotalCount { get; set; }

        public int Page {  get; set; }
        public int Limit { get; set; }

        public List<T> Data { get; set; }




    }
}
