using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using Ecommerce12.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ecommerce12.DAL.DTO_s.Response.Product;
using Ecommerce12.DAL.DTO_s.Response.Order;
using Stripe;


namespace Ecommerce12.BLL.MapsterConfigration
{
    public static class MapsterConfig
    {
        public static void  MapterConfigRegiter()
        {
              TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest =>dest.CreatedBy , source => source.User.FullName);


              TypeAdapterConfig<Category, CategoryResponseForUser>.NewConfig()
             .Map(dest => dest.Name, src => src.Translation
             .Where(t => t.Lang == MapContext.Current.Parameters["Language"].ToString())
             .Select(t => t.Name).FirstOrDefault());

            TypeAdapterConfig<DAL.Models.Product, ProductsResponseUser>.NewConfig()
                .Map(dest => dest.MainImage , src => $"https://localhost:7277/images/{src.MainImage}");


            TypeAdapterConfig<DAL.Models.Product, ProductsResponseUser>.NewConfig()
                .Map(dest => dest.MainImage, source => $"https://localhost:7277/images/{source.MainImage}")
                .Map(dest => dest.Name , src => src.Translations
                .Where(t=>t.Language == MapContext.Current.Parameters["Language"].ToString())
                .Select(t =>t.Name).FirstOrDefault());


            TypeAdapterConfig<DAL.Models.Product, ProductUserDetailsResponse>.NewConfig()
           .Map(dest => dest.Name, src => src.Translations
           .Where(t => t.Language == MapContext.Current.Parameters["Language"].ToString())
           .Select(t => t.Name).FirstOrDefault())
            .Map(dest => dest.Description, src => src.Translations
           .Where(t => t.Language == MapContext.Current.Parameters["Language"].ToString())
           .Select(t => t.Description).FirstOrDefault());

            TypeAdapterConfig<Order, OrderResponse>.NewConfig().Map(dest => dest.UserName , src => src.User.UserName);

            TypeAdapterConfig<DAL.Models.Review, ReviewsResponse>.NewConfig().Map(dest => dest.Name, src => src.User.UserName);


        }
    }
}
