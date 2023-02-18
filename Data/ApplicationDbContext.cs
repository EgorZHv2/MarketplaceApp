using Data.Entities;
using Data.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<StaticFileInfo> StaticFileInfos => Set<StaticFileInfo>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Data.Entities.Type> Types => Set<Data.Entities.Type>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public DbSet<DeliveryType> DeliveryTypes => Set<DeliveryType>();

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