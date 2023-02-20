using Data.Entities;
using Data.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ShopEntity> Shops => Set<ShopEntity>();
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<StaticFileInfoEntity> StaticFileInfos => Set<StaticFileInfoEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<TypeEntity> Types => Set<TypeEntity>();
        public DbSet<PaymentMethodEntity> PaymentMethods => Set<PaymentMethodEntity>();
        public DbSet<DeliveryTypeEntity> DeliveryTypes => Set<DeliveryTypeEntity>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new StaticFileInfoConfiguration());
            modelBuilder.ApplyConfiguration(new TypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}