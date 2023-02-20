using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class StaticFileInfoConfiguration : IEntityTypeConfiguration<StaticFileInfoEntity>
    {
        public void Configure(EntityTypeBuilder<StaticFileInfoEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(staticFileInfo => staticFileInfo.Id);
        }
    }
}