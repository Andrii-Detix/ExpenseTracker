using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(u => u.Id)
            .HasName("pk_users");

        builder.Property(u => u.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        builder.Property(u => u.DefaultCurrencyId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("default_currency_id");

        builder.OwnsOne(u => u.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName("name");
        });

        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(u => u.DefaultCurrencyId);
    }
}