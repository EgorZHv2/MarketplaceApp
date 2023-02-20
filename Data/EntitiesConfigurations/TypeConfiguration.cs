using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<TypeEntity>
    {
        public void Configure(EntityTypeBuilder<TypeEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(type => type.Id);
            entityTypeBuilder.HasQueryFilter(type => type.IsDeleted == false);
            entityTypeBuilder.Property(type => type.Description).HasMaxLength(500);
        }
    }
}