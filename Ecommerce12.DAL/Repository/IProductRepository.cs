using Ecommerce12.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.DAL.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        IQueryable<Product> Query();
        Task<Product?> FindByIdAsync(int id);
        Task<bool> DecreaseQuantitiesAsync(List<(int productId, int quantity)> items);

    }
}
