﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder
                .HasOne(e => e.Shop)
                .WithMany(t => t.Reviews)
                .HasForeignKey(e => e.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
            entityTypeBuilder
                .HasOne(e => e.Buyer)
                .WithMany(t => t.Reviews)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.Cascade);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}