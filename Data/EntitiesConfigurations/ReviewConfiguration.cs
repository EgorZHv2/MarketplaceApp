using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(review => review.Id);
            entityTypeBuilder
                .HasOne(review => review.Shop)
                .WithMany(shop => shop.Reviews)
                .HasForeignKey(review => review.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
            entityTypeBuilder
                .HasOne(review => review.Buyer)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.BuyerId)
                .OnDelete(DeleteBehavior.Cascade);
            entityTypeBuilder.HasQueryFilter(review => review.IsDeleted == false);
        }
    }
}