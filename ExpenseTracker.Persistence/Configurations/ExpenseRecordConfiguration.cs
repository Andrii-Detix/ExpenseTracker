using ExpenseTracker.Domain.Categories;
using ExpenseTracker.Domain.ExpenseRecords;
using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configurations;

public class ExpenseRecordConfiguration : IEntityTypeConfiguration<ExpenseRecord>
{
    public void Configure(EntityTypeBuilder<ExpenseRecord> builder)
    {
        builder.ToTable("expense_records");
        
        builder.HasKey(er => er.Id)
            .HasName("pk_expense_records");
        
        builder.Property(er => er.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        builder.Property(er => er.UserId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("user_id");
        
        builder.Property(er => er.CategoryId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("category_id");
        
        builder.Property(er => er.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.ComplexProperty(er => er.Amount, amountBuilder =>
        {
            amountBuilder.Property(a => a.Value)
                .IsRequired()
                .HasColumnName("amount");
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(er => er.UserId);
        
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(er => er.CategoryId);
    }
}