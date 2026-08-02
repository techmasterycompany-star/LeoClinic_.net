using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class PatientProfileConfiguration : IEntityTypeConfiguration<PatientProfile>
{
    public void Configure(EntityTypeBuilder<PatientProfile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ContactNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.DateOfBirth)
            .IsRequired();

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.IsApproved)
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(u => u.PatientProfile)
            .HasForeignKey<PatientProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
