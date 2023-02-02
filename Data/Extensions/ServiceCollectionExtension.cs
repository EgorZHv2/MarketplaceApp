using Data.IRepositories;
using Data.Repositories;
using Data.Repositories.DictionaryRepositories;
using Data.Repositories.Repositories;
using Data.Repositories.ShopDictionaryRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions
{
    public static partial class ServiceCollectionExtension
    {
        public static void AddAppDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostreSQL"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IStaticFileInfoRepository, StaticFileInfoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUsersFavoriteShopsRepository, UsersFavoriteShopsRepository>();

            services.AddScoped(typeof(IBaseDictionaryRepository<>), typeof(BaseDictionaryRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDeliveryTypeRepository, DeliveryTypeRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();

            services.AddScoped<IShopCategoryRepository, ShopCategoryRepository>();
            services.AddScoped<IShopDeliveryTypeRepository, ShopDeliveryTypeRepository>();
            services.AddScoped<IShopPaymentMethodRepository, ShopPaymentMethodRepository>();
            services.AddScoped<IShopTypeRepository, ShopTypeRepository>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}