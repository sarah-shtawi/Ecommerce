using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Models
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; } = "en";
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
