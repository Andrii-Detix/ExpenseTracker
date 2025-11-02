using ExpenseTracker.Domain.Currencies;
using ExpenseTracker.Domain.Users;
using ExpenseTracker.Domain.Users.ValueObjects.PasswordHashes;
using ExpenseTracker.Domain.Users.ValueObjects.UserLogins;
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
        
        builder.HasIndex(u => u.Login)
            .IsUnique()
            .HasDatabaseName("ux_users_login");

        builder.Property(u => u.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(u => u.Login)
            .HasConversion(
                l => l.Value,
                value => UserLogin.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("login");
        
        builder.Property(u => u.PasswordHash)
            .HasConversion(
                ph => ph.Value,
                value => PasswordHash.Create(value).Value!)
            .IsRequired()
            .HasColumnName("password_hash");

        builder.OwnsOne(u => u.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(400)
                .HasColumnName("name");
        });
        
        builder.Property(u => u.DefaultCurrencyId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("default_currency_id");

        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(u => u.DefaultCurrencyId);
    }
}