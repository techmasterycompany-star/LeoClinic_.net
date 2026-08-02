using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
{
    public void Configure(EntityTypeBuilder<DoctorProfile> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(d => d.Bio)
            .IsRequired();

        builder.Property(d => d.ContactNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.IsApproved)
            .IsRequired();

        builder.HasOne(d => d.User)
            .WithOne(u => u.DoctorProfile)
            .HasForeignKey<DoctorProfile>(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Speciality)
            .WithMany()
            .HasForeignKey(d => d.SpecialtyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
