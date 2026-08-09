using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<PatientProfile> PatientProfiles { get; set; }

        public DbSet<DoctorProfile> DoctorProfiles { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<DoctorLocation> DoctorLocations { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<VerificationCode> VerificationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.PatientProfile)
                .HasForeignKey<PatientProfile>(p => p.UserId);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.User)
                .WithOne(u => u.DoctorProfile)
                .HasForeignKey<DoctorProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.DoctorProfiles)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorLocation>()
                .HasOne(dl => dl.Doctor)
                .WithMany(d => d.DoctorLocations)
                .HasForeignKey(dl => dl.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorLocation>()
                .HasOne(dl => dl.Location)
                .WithMany(l => l.DoctorLocations)
                .HasForeignKey(dl => dl.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Location)
                .WithMany(l => l.Availabilities)
                .HasForeignKey(a => a.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Availability)
                .WithMany(av => av.Appointments)
                .HasForeignKey(a => a.AvailabilityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Appointment)
                .WithMany(a => a.Notifications)
                .HasForeignKey(n => n.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany(pp => pp.Payments)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Ratings)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VerificationCode>()
                .HasOne(v => v.User)
                .WithMany(u => u.VerificationCodes)
                .HasForeignKey(v => v.UserId);
        }
    }
}
