using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Shop> Shops { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<UsersFavShops> UsersFavShops { get; set; } = null!;
        public DbSet<StaticFileInfo> StaticFileInfos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Data.Entities.Type> Types { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<DeliveryType> DeliveryTypes { get; set; } = null!;
        
        public ApplicationDbContext(DbContextOptions options):base(options)        
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(e => e.FirstName).IsRequired(false);
            modelBuilder.Entity<User>().HasQueryFilter(e => e.IsDeleted == false);
            modelBuilder.Entity<User>().Property(e=>e.CreatorId).IsRequired(false);
            modelBuilder.Entity<User>().Property(e=>e.UpdatorId).IsRequired(false);
            modelBuilder.Entity<User>().Property(e=>e.EmailConfirmationCode).IsRequired(false);

            modelBuilder.Entity<Shop>().HasKey(x =>x.Id);
            modelBuilder.Entity<Shop>().HasOne(e => e.Seller).WithMany(t => t.Shops).HasForeignKey(e => e.SellerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shop>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<Review>().HasKey(x =>x.Id);
            modelBuilder.Entity<Review>().HasOne(e => e.Shop).WithMany(t=> t.Reviews).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasOne(e => e.Buyer).WithMany(t => t.Reviews).HasForeignKey(e => e.BuyerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<UsersFavShops>().HasKey(x => x.Id);
            modelBuilder.Entity<UsersFavShops>().HasOne(e => e.User).WithMany(t=>t.UsersFavShops).HasForeignKey(e=>e.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersFavShops>().HasOne(e=>e.Shop).WithMany(t=>t.UsersFavShops).HasForeignKey(e=>e.ShopId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersFavShops>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<StaticFileInfo>().HasKey(x => x.Id);
            modelBuilder.Entity<UsersFavShops>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasQueryFilter(e => e.IsDeleted == false);
            modelBuilder.Entity<Category>().HasMany(e => e.Shops).WithMany(e => e.Categories).UsingEntity(e => e.ToTable("ShopCategories"));

            modelBuilder.Entity<Data.Entities.Type>().HasKey(x => x.Id);
            modelBuilder.Entity<Data.Entities.Type>().HasQueryFilter(e => e.IsDeleted == false);
            modelBuilder.Entity<Data.Entities.Type>().HasMany(e => e.Shops).WithMany(e => e.Types).UsingEntity(e => e.ToTable("ShopTypes"));
            
            modelBuilder.Entity<PaymentMethod>().HasKey(x => x.Id);
            modelBuilder.Entity<PaymentMethod>().HasQueryFilter(e => e.IsDeleted == false);
            modelBuilder.Entity<PaymentMethod>().HasMany(e => e.Shops).WithMany(e => e.PaymentMethods).UsingEntity(e => e.ToTable("ShopPatmentMethods"));

            modelBuilder.Entity<DeliveryType>().HasKey(x => x.Id);
            modelBuilder.Entity<DeliveryType>().HasQueryFilter(e => e.IsDeleted == false);
            modelBuilder.Entity<DeliveryType>().HasMany(e => e.Shops).WithMany(e => e.DeliveryTypes).UsingEntity(e => e.ToTable("ShopDeliveryTypes"));
        }

    }
}
