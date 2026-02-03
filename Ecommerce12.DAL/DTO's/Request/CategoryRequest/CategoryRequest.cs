using Ecommerce12.DAL.DTO_s.Request.CategoryRequest;
using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.Category
{
    public class CategoryRequest
    {
       public List <CategoryTranslationsRequest> Translation { get; set; }

       
    }
}
