using Ecommerce12.BLL.Service;
using Ecommerce12.DAL.Repository;
using Ecommerce12.DAL.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce12.PL
{
    public static class AppConfigrations
    {

        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();

            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();

            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IProductService, ProductService>();

            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddScoped<ICartRepository, CartRepository>();
            Services.AddScoped<ICartService, CartService>();

             Services.AddScoped<ICheckOutService, CheckOutService>();

            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            Services.AddScoped<IOrderService, OrderService>();

            Services.AddScoped<IManageUserService, ManageUserService>();

            Services.AddScoped<IReviewService, ReviewService>();
            Services.AddScoped<IReviewRepository, ReviewRepository>();
            


            Services.AddExceptionHandler<GlobalExcpetionHandler>();
            Services.AddProblemDetails();


        }
    }
}
