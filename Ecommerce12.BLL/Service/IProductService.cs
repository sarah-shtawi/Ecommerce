using Ecommerce12.DAL.DTO_s.Request.Product;
using Ecommerce12.DAL.DTO_s.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public  interface IProductService
    {
        Task<List<ProductResponse>> GetProducts();
        public Task<ProductResponse> CreateProduct(ProductRequest request);

         Task<PagintedResponse<ProductsResponseUser>> GetAllProductsForUser(string Language = "en",
          int page = 1, int limit = 3, string? search = null, int? categoryId = null, decimal? MinPrice = null, decimal? MaxPrice = null, string? sortBy = null, bool asc = true);
        Task <ProductUserDetailsResponse> GetProductDetails(int id, string Language = "en");
    }
}
