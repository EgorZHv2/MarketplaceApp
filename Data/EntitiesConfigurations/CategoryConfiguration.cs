using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasQueryFilter(e => e.IsDeleted == false);
            entityTypeBuilder.Property(e => e.ParentCategoryId).IsRequired(false);
            entityTypeBuilder
                .HasOne(e => e.ParentCategory)
                .WithMany(e => e.Categories)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}