using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeoClinic.Infrastructure.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Message)
            .IsRequired();

        builder.Property(n => n.Type)
            .IsRequired();

        builder.Property(n => n.Status)
            .IsRequired();

        builder.Property(n => n.SentAt)
            .IsRequired();

        builder.HasIndex(n => n.UserId);

        builder.HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Appointment)
            .WithMany(a => a.Notifications)
            .HasForeignKey(n => n.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
