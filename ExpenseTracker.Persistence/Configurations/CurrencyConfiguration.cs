using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Currencies.ValueObjects.Codes;
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
        
        builder.HasIndex(c => c.Code)
            .IsUnique();
        
        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(c => c.Code)
            .HasConversion(
                cc => cc.Value, 
                value => CurrencyCode.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(3)
            .IsFixedLength()
            .HasColumnName("code");

        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(cn => cn.Value)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName("name");
        });
    }
}