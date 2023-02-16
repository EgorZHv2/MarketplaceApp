using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(deliveryType => deliveryType.Id);
            entityTypeBuilder.HasQueryFilter(deliveryType => deliveryType.IsDeleted == false);
        }
    }
}