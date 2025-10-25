using ExpenseTracker.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        
        builder.HasKey(c => c.Id)
            .HasName("pk_categories");
        
        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName("name");
        });
    }
}