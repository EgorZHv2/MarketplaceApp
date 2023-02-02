using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder
                .HasOne(e => e.Seller)
                .WithMany(t => t.Shops)
                .HasForeignKey(e => e.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);

            entityTypeBuilder
                .HasMany<DeliveryType>(e => e.DeliveryTypes)
                .WithMany(e => e.Shops)
                .UsingEntity<ShopDeliveryType>(e =>
                {
                    e.HasOne(e => e.Shop)
                        .WithMany(e => e.ShopDeliveryTypes)
                        .HasForeignKey(e => e.ShopId);
                    e.HasOne(e => e.DeliveryType)
                        .WithMany(e => e.ShopDeliveryTypes)
                        .HasForeignKey(e => e.DeliveryTypeId);
                    e.ToTable("ShopDeliveryTypes");
                    e.Property(p => p.FreeDeliveryThreshold).IsRequired(false);
                });
            entityTypeBuilder
                .HasMany<PaymentMethod>(e => e.PaymentMethods)
                .WithMany(e => e.Shops)
                .UsingEntity<ShopPaymentMethod>(e =>
                {
                    e.HasOne(e => e.Shop)
                        .WithMany(e => e.ShopPaymentMethods)
                        .HasForeignKey(e => e.ShopId);
                    e.HasOne(e => e.PaymentMethod)
                        .WithMany(e => e.ShopPaymentMethods)
                        .HasForeignKey(e => e.PaymentMethodId);
                    e.ToTable("ShopPaymentMethods");
                    e.Property(p => p.Сommission).IsRequired(false);
                });
            entityTypeBuilder
               .HasMany<Category>(e => e.Categories)
               .WithMany(e => e.Shops)
               .UsingEntity<ShopCategory>(e =>
               {
                   e.HasOne(e => e.Shop)
                       .WithMany(e => e.ShopCategories)
                       .HasForeignKey(e => e.ShopId);
                   e.HasOne(e => e.Category)
                       .WithMany(e => e.ShopCategories)
                       .HasForeignKey(e => e.CategoryId);
                   e.ToTable("ShopCategories");
               });
            entityTypeBuilder
                .HasMany<Data.Entities.Type>(e => e.Types)
                .WithMany(e => e.Shops)
                .UsingEntity<ShopType>(e =>
                {
                    e.HasOne(e => e.Shop)
                        .WithMany(e => e.ShopTypes)
                        .HasForeignKey(e => e.ShopId);
                    e.HasOne(e => e.Type)
                        .WithMany(e => e.ShopTypes)
                        .HasForeignKey(e => e.TypeId);
                    e.ToTable("ShopTypes");
                });
        }
    }
}