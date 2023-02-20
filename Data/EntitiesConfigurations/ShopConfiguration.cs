using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<ShopEntity>
    {
        public void Configure(EntityTypeBuilder<ShopEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(shop => shop.Id);
            entityTypeBuilder
                .HasOne(shop => shop.Seller)
                .WithMany(user => user.Shops)
                .HasForeignKey(shop => shop.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.HasQueryFilter(shop => shop.IsDeleted == false);

            entityTypeBuilder
                .HasMany<DeliveryTypeEntity>(shop => shop.DeliveryTypes)
                .WithMany(deliveryType => deliveryType.Shops)
                .UsingEntity<ShopDeliveryTypeEntity>(shopDeliveryType =>
                {
                    shopDeliveryType.HasOne(shopDeliveryType => shopDeliveryType.Shop)
                        .WithMany(shop => shop.ShopDeliveryTypes)
                        .HasForeignKey(shopDeliveryType => shopDeliveryType.ShopId);
                    shopDeliveryType.HasOne(shopDeliveryType => shopDeliveryType.DeliveryType)
                        .WithMany(deliveryType => deliveryType.ShopDeliveryTypes)
                        .HasForeignKey(shopDeliveryType => shopDeliveryType.DeliveryTypeId);
                    shopDeliveryType.ToTable("ShopDeliveryTypes");
                    shopDeliveryType.Property(shopDeliveryType => shopDeliveryType.FreeDeliveryThreshold).IsRequired(false);
                });

            entityTypeBuilder
                .HasMany<PaymentMethodEntity>(shop => shop.PaymentMethods)
                .WithMany(paymentMethod => paymentMethod.Shops)
                .UsingEntity<ShopPaymentMethodEntity>(shopPaymentMethod =>
                {
                    shopPaymentMethod.HasOne(shopPaymentMethod => shopPaymentMethod.Shop)
                        .WithMany(shop => shop.ShopPaymentMethods)
                        .HasForeignKey(shopPaymentMethod => shopPaymentMethod.ShopId);
                    shopPaymentMethod.HasOne(shopPaymentMethod=> shopPaymentMethod.PaymentMethod)
                        .WithMany(paymentMethod => paymentMethod.ShopPaymentMethods)
                        .HasForeignKey(shopPaymentMethod => shopPaymentMethod.PaymentMethodId);
                    shopPaymentMethod.ToTable("ShopPaymentMethods");
                    shopPaymentMethod.Property(shopPaymentMethod => shopPaymentMethod.Сommission).IsRequired(false);
                });

            entityTypeBuilder
               .HasMany<CategoryEntity>(shop => shop.Categories)
               .WithMany(category => category.Shops)
               .UsingEntity<ShopCategoryEntity>(shopCategory =>
               {
                   shopCategory.HasOne(shopCategory => shopCategory.Shop)
                       .WithMany(shop => shop.ShopCategories)
                       .HasForeignKey(shopCategory => shopCategory.ShopId);
                   shopCategory.HasOne(shopCategory => shopCategory.Category)
                       .WithMany(category => category.ShopCategories)
                       .HasForeignKey(shopCategory => shopCategory.CategoryId);
                   shopCategory.ToTable("ShopCategories");
               });

            entityTypeBuilder
                .HasMany<TypeEntity>(shop => shop.Types)
                .WithMany(type => type.Shops)
                .UsingEntity<ShopTypeEntity>(shopType =>
                {
                    shopType.HasOne(shopType => shopType.Shop)
                        .WithMany(shop => shop.ShopTypes)
                        .HasForeignKey(shopType => shopType.ShopId);
                    shopType.HasOne(shopType => shopType.Type)
                        .WithMany(type => type.ShopTypes)
                        .HasForeignKey(shopType => shopType.TypeId);
                    shopType.ToTable("ShopTypes");
                });
        }
    }
}