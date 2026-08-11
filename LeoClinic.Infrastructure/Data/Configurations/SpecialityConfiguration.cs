using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .IsRequired();

        builder.HasData(
            new Speciality { Id = 1, Name = "General Practice", Description = "General medical practice", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Speciality { Id = 2, Name = "Cardiology", Description = "Heart and cardiovascular system", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Speciality { Id = 3, Name = "Dermatology", Description = "Skin, hair, and nails", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Speciality { Id = 4, Name = "Pediatrics", Description = "Children's health", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Speciality { Id = 5, Name = "Orthopedics", Description = "Bones, joints, and muscles", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
