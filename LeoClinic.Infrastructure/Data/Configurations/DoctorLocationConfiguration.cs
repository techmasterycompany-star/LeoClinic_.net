using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class DoctorLocationConfiguration : IEntityTypeConfiguration<DoctorLocation>
{
    public void Configure(EntityTypeBuilder<DoctorLocation> builder)
    {
        builder.HasKey(dl => dl.Id);

        builder.HasOne(dl => dl.DoctorProfile)
            .WithMany(d => d.DoctorLocations)
            .HasForeignKey(dl => dl.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dl => dl.Location)
            .WithMany(l => l.DoctorLocations)
            .HasForeignKey(dl => dl.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
