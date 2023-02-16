using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Data.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Data.Entities.Type> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(type => type.Id);
            entityTypeBuilder.HasQueryFilter(type => type.IsDeleted == false);
            entityTypeBuilder.Property(type => type.Description).HasMaxLength(500);
        }
    }
}