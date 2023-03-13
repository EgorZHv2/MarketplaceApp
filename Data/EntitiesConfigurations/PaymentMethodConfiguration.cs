using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(paymentMethod => paymentMethod.Id);
            entityTypeBuilder.HasQueryFilter(paymentMethod => paymentMethod.DeleteDateTime == null);
        }
    }
}