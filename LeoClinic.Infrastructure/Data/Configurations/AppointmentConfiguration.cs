using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.Notes)
            .IsRequired();

        builder.HasOne(a => a.PatientProfile)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.DoctorProfile)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Availability)
            .WithMany(av => av.Appointments)
            .HasForeignKey(a => a.AvailabilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.AvailabilityId)
            .IsUnique()
            .HasFilter("[Status] <> 3 AND [Status] <> 4");
    }
}
