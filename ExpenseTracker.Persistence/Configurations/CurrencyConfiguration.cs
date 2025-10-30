using ExpenseTracker.Domain.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currencies");
        
        builder.HasKey(c => c.Id)
            .HasName("pk_currencies");
        
        builder.HasIndex(c => c.Code.Value)
            .IsUnique();
        
        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.ComplexProperty(c => c.Code, codeBuilder =>
        {
            codeBuilder.Property(cc => cc)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("code");
        });

        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(cn => cn.Value)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName("name");
        });
    }
}