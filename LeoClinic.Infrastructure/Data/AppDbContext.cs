using LeoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeoClinic.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();
        public DbSet<DoctorProfile> DoctorProfiles => Set<DoctorProfile>();
        public DbSet<PatientProfile> PatientProfiles => Set<PatientProfile>();
        public DbSet<Speciality> Specialties => Set<Speciality>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<DoctorLocation> DoctorLocations => Set<DoctorLocation>();
        public DbSet<Availability> Availabilities => Set<Availability>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
