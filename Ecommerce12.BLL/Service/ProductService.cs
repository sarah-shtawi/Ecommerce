using Ecommerce12.DAL.DTO_s.Request.Product;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.Product;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce12.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        public ProductService(IProductRepository ProductRepository, IFileService fileService)
        {
            _productRepository = ProductRepository;
            _fileService = fileService;
        }
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();

            if(request.MainImage != null)
            {
                var imagePath = await _fileService.UploadeFile(request.MainImage);
                product.MainImage = imagePath;
            }

            if (request.SubImages != null)
            {
                product.SubImages = new List<ProductImage>();
                foreach (var image in request.SubImages)
                {
                    var imagePath = await _fileService.UploadeFile(image);
                    product.SubImages.Add( new ProductImage
                    {
                        ImageName = imagePath
                    });
                }
            }
           var ProductDB = await _productRepository.CreateProductAsync(product);
           var response = ProductDB.Adapt<ProductResponse>();   
           return response;
        }
        public async Task<List<ProductResponse>> GetProducts()
        {
            var products =  _productRepository.Query();
            var productsRES = products.Adapt<List<ProductResponse>>();
            return productsRES;
        
        }
        public async Task<PagintedResponse<ProductsResponseUser>> GetAllProductsForUser(string Language = "en", int page = 1 , int limit = 3 ,
            string? search = null , int? categoryId = null , decimal? MinPrice = null , decimal? MaxPrice = null ,
            string? sortBy=null ,bool asc=true)
        {
            var query = _productRepository.Query();
            if (search is not null)
            {
                query= query.Where(p => p.Translations.Any(t => t.Language == Language && (t.Name.Contains(search) || t.Description.Contains(search))));
       
            }
            if (categoryId is not null)
            {
                query = query.Where(p=>p.CategoryId == categoryId);
            }
            if(MinPrice is not null)
            {
                query = query.Where(p=>p.Price >=MinPrice);
            }
            if (MaxPrice is not null)
            {
                query = query.Where(p => p.Price <= MaxPrice);
            }
            //sort 
            if (sortBy is not null)
            {
                sortBy = sortBy.ToLower();
                if(sortBy == "price")
                {
                    query = asc ? query.OrderBy(p=>p.Price) : query.OrderByDescending(p=>p.Price);
                }
                else if(sortBy == "rate")
                {
                    query = asc ? query.OrderBy(p=>p.Rate) : query.OrderByDescending(p=>p.Rate);
                }
                else if(sortBy == "name")
                {
                    query = asc ? query.OrderBy(p => p.Translations.FirstOrDefault(t=>t.Language == Language).Name)
                        
                  : query.OrderByDescending(p => p.Translations.FirstOrDefault(t=>t.Language == Language).Name);
                }
            }
            var totalCount = await query.CountAsync();
            query = query.Skip((page - 1) *limit ).Take(limit);
            
            var products =  query.BuildAdapter().AddParameters("Language", Language).AdaptToType<List<ProductsResponseUser>>();
            return new PagintedResponse<ProductsResponseUser>
            {
                TotalCount = totalCount,
                Page = page,
                Limit = limit,
                Data = products
            };
        }
        public async Task<ProductUserDetailsResponse> GetProductDetails(int id , string Language = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);
            var response  = product.BuildAdapter().AddParameters("Language", Language).AdaptToType<ProductUserDetailsResponse>();
            return response;
        }
    }
}
