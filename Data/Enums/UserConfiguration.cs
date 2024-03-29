﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(user => user.Id);
            entityTypeBuilder.Property(user => user.FirstName).IsRequired(false);
            entityTypeBuilder.HasQueryFilter(user => user.DeleteDateTime == null);
            entityTypeBuilder.Property(user => user.CreatedBy).IsRequired(false);
            entityTypeBuilder.Property(user => user.UpdatedBy).IsRequired(false);
            entityTypeBuilder.Property(user => user.DeletedBy).IsRequired(false);
            entityTypeBuilder.Property(user => user.EmailConfirmationCode).IsRequired(false);
            entityTypeBuilder
                .HasMany(user => user.FavoriteShops)
                .WithMany(shop => shop.Users)
                .UsingEntity<UserFavoriteShopEntity>(userFavoriteShop =>
                {
                    userFavoriteShop.HasOne(userFavoriteShop => userFavoriteShop.Shop)
                        .WithMany(shop => shop.UserFavoriteShops)
                        .HasForeignKey(userFavoriteShop => userFavoriteShop.ShopId);
                    userFavoriteShop.HasOne(userFavoriteShop => userFavoriteShop.User)
                        .WithMany(user => user.UsersFavoriteShops)
                        .HasForeignKey(userFavoriteShop => userFavoriteShop.UserId);
                    userFavoriteShop.ToTable("UsersFavoriteShops");
                });
        }
    }
}