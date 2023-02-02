using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}