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
        
        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.OwnsOne(c => c.Code, codeBuilder =>
        {
            codeBuilder.HasIndex(cc => cc.Value)
                .IsUnique();
            
            codeBuilder.Property(cc => cc.Value)
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