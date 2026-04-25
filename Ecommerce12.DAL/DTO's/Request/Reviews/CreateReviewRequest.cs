using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.DTO_s.Request.Reviews
{
    public  class CreateReviewRequest
    {

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }


        [Required]
        [MaxLength(5)]
        public string Comment { get; set; }
    }
}
