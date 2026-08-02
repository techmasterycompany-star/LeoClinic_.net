using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Token)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.Type)
            .IsRequired();

        builder.Property(v => v.ExpiresAt)
            .IsRequired();

        builder.HasOne(v => v.User)
            .WithMany(u => u.VerificationCodes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
