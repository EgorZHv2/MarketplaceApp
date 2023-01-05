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
        
        public ApplicationDbContext(DbContextOptions options):base(options)        
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(e => e.FirstName).IsRequired(false);
            modelBuilder.Entity<User>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<Shop>().HasKey(x =>x.Id);
            modelBuilder.Entity<Shop>().HasOne(e => e.Seller).WithMany(t => t.Shops).HasForeignKey(e => e.SellerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shop>().Property(e => e.Logo).IsRequired(false);
             modelBuilder.Entity<Shop>().HasQueryFilter(e => e.IsDeleted == false);

            modelBuilder.Entity<Review>().HasKey(x =>x.Id);
            modelBuilder.Entity<Review>().HasOne(e => e.Shop).WithMany(t=> t.Reviews).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasOne(e => e.Buyer).WithMany(t => t.Reviews).HasForeignKey(e => e.BuyerId).OnDelete(DeleteBehavior.Cascade);
             modelBuilder.Entity<Review>().HasQueryFilter(e => e.IsDeleted == false);
        }

    }
}
