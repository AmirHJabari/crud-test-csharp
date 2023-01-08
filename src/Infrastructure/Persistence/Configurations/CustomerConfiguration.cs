using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(b => b.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(b => b.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(b => b.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.BankAccountNumber)
            .HasMaxLength(16)
            .IsRequired();

        // Indexes
        builder.HasIndex(b => b.Email)
            .IsUnique();

        builder.HasIndex(x => new { x.FirstName, x.LastName, x.DateOfBirth })
            .IsUnique();
    }
}
