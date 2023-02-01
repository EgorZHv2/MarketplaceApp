using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntitiesConfigurations
{
    public class StaticFileInfoConfiguration:IEntityTypeConfiguration<StaticFileInfo>
    {
        public void Configure(EntityTypeBuilder<StaticFileInfo> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
        }
    }
}
