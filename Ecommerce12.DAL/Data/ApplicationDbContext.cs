using Ecommerce12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Data
{
    public class ApplicationDbContext: DbContext
    {

         // public ApplicationDbContext() :base() { }
         public DbSet<Category> categories {  get; set; }
        
         public DbSet<CategoryTranslation> categoriesTranslation { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }





    }
}
