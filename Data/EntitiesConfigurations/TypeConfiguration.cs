using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Data.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Data.Entities.Type> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);
            entityTypeBuilder.Property(e => e.Description).HasMaxLength(500);
        }
    }
}