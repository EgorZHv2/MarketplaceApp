using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class StaticFileInfoConfiguration : IEntityTypeConfiguration<StaticFileInfo>
    {
        public void Configure(EntityTypeBuilder<StaticFileInfo> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(staticFileInfo => staticFileInfo.Id);
        }
    }
}