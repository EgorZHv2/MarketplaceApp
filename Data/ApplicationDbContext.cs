using Data.Entities;
using Data.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<StaticFileInfo> StaticFileInfos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Data.Entities.Type> Types { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<DeliveryType> DeliveryTypes { get; set; } = null!;

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