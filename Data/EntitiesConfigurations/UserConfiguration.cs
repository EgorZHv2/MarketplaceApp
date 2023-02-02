using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(e => e.FirstName).IsRequired(false);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);
            entityTypeBuilder.Property(e => e.CreatorId).IsRequired(false);
            entityTypeBuilder.Property(e => e.UpdatorId).IsRequired(false);
            entityTypeBuilder.Property(e => e.EmailConfirmationCode).IsRequired(false);
            entityTypeBuilder
                .HasMany(e => e.FavoriteShops)
                .WithMany(e => e.Users)
                .UsingEntity<UserFavoriteShop>(e =>
                {
                    e.HasOne(e => e.Shop)
                        .WithMany(e => e.UserFavoriteShops)
                        .HasForeignKey(e => e.ShopId);
                    e.HasOne(e => e.User)
                        .WithMany(e => e.UsersFavoriteShops)
                        .HasForeignKey(e => e.UserId);
                    e.ToTable("UsersFavoriteShops");
                });
        }
    }
}