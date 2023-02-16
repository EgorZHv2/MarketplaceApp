using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(paymentMethod => paymentMethod.Id);
            entityTypeBuilder.HasQueryFilter(paymentMethod => paymentMethod.IsDeleted == false);
        }
    }
}