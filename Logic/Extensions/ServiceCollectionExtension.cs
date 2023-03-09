using Logic.Interfaces;
using Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using WebAPi.Interfaces;
using WebAPi.Mappers;
using WebAPi.Services;

namespace Logic.Extensions
{
    public static partial class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDeliveryTypeService, DeliveryTypeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IINNService, INNService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IRandomStringGeneratorService, RandomStringGeneratorService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IFileService,FIleService>();
            services.AddScoped<IXMLService, XMLService>();
        }
    }
}