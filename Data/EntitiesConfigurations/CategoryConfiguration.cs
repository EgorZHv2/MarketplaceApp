using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(category => category.Id);
            entityTypeBuilder.HasQueryFilter(category => category.IsDeleted == false);
            entityTypeBuilder.Property(category => category.ParentCategoryId).IsRequired(false);
            entityTypeBuilder
                .HasOne(category => category.ParentCategory)
                .WithMany(category => category.Categories)
                .HasForeignKey(category => category.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}