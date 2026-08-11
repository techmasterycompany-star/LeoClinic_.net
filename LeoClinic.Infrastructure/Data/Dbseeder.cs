using LeoClinic.Domain.Entities;
using LeoClinic.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LeoClinic.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Prevent duplicate test data
            if (await context.Users.AnyAsync(u => u.Email == "admin@leoclinic.com"))
            {
                return;
            }

            var now = DateTime.UtcNow;

            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // ============================================================
                // 1. SPECIALTIES
                // ============================================================

                var cardiology = new Speciality
                {
                    Name = "Cardiology",
                    Description = "Diagnosis and treatment of heart and cardiovascular conditions",
                    CreatedAt = now
                };

                var dermatology = new Speciality
                {
                    Name = "Dermatology",
                    Description = "Diagnosis and treatment of skin, hair and nail conditions",
                    CreatedAt = now
                };

                var pediatrics = new Speciality
                {
                    Name = "Pediatrics",
                    Description = "Medical care for infants, children and adolescents",
                    CreatedAt = now
                };

                var dentistry = new Speciality
                {
                    Name = "Dentistry",
                    Description = "Prevention and treatment of dental conditions",
                    CreatedAt = now
                };

                await context.Specialties.AddRangeAsync(
                    cardiology,
                    dermatology,
                    pediatrics,
                    dentistry);

                await context.SaveChangesAsync();


                // ============================================================
                // 2. LOCATIONS
                // ============================================================

                var cairoClinic = new Location
                {
                    Name = "LeoClinic Cairo",
                    Address = "15 Tahrir Street",
                    City = "Cairo",
                    Phone = "01000000001",
                    CreatedAt = now
                };

                var gizaClinic = new Location
                {
                    Name = "LeoClinic Giza",
                    Address = "20 Faisal Street",
                    City = "Giza",
                    Phone = "01000000002",
                    CreatedAt = now
                };

                var alexClinic = new Location
                {
                    Name = "LeoClinic Alexandria",
                    Address = "10 Alexandria Corniche",
                    City = "Alexandria",
                    Phone = "01000000003",
                    CreatedAt = now
                };

                await context.Locations.AddRangeAsync(
                    cairoClinic,
                    gizaClinic,
                    alexClinic);

                await context.SaveChangesAsync();


                // ============================================================
                // 3. USERS
                // ============================================================

                var admin = new User
                {
                    Email = "admin@leoclinic.com",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = UserRole.Admin,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var doctorUser1 = new User
                {
                    Email = "doctor1@leoclinic.com",
                    FirstName = "Ahmed",
                    LastName = "Hassan",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                    Role = UserRole.Doctor,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var doctorUser2 = new User
                {
                    Email = "doctor2@leoclinic.com",
                    FirstName = "Mona",
                    LastName = "Ali",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                    Role = UserRole.Doctor,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var doctorUser3 = new User
                {
                    Email = "doctor3@leoclinic.com",
                    FirstName = "Omar",
                    LastName = "Ibrahim",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                    Role = UserRole.Doctor,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var patientUser1 = new User
                {
                    Email = "patient1@leoclinic.com",
                    FirstName = "Karim",
                    LastName = "Mohamed",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Patient123!"),
                    Role = UserRole.Patient,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var patientUser2 = new User
                {
                    Email = "patient2@leoclinic.com",
                    FirstName = "Sara",
                    LastName = "Mahmoud",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Patient123!"),
                    Role = UserRole.Patient,
                    IsBlocked = false,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                var patientUser3 = new User
                {
                    Email = "patient3@leoclinic.com",
                    FirstName = "Youssef",
                    LastName = "Khaled",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Patient123!"),
                    Role = UserRole.Patient,
                    IsBlocked = true,
                    EmailConfirmed = true,
                    CreatedAt = now
                };

                await context.Users.AddRangeAsync(
                    admin,
                    doctorUser1,
                    doctorUser2,
                    doctorUser3,
                    patientUser1,
                    patientUser2,
                    patientUser3);

                await context.SaveChangesAsync();


                // ============================================================
                // 4. DOCTOR PROFILES
                // ============================================================

                var doctor1 = new DoctorProfile
                {
                    UserId = doctorUser1.Id,
                    SpecialityId = cardiology.Id,
                    Price = 500m,
                    Bio = "Experienced cardiologist specializing in cardiovascular diseases.",
                    ContactNumber = "01111111111",
                    IsApproved = true,
                    CreatedAt = now
                };

                var doctor2 = new DoctorProfile
                {
                    UserId = doctorUser2.Id,
                    SpecialityId = dermatology.Id,
                    Price = 400m,
                    Bio = "Dermatologist specializing in skin and hair conditions.",
                    ContactNumber = "01111111112",
                    IsApproved = true,
                    CreatedAt = now
                };

                // Pending doctor - useful for testing doctor approval
                var doctor3 = new DoctorProfile
                {
                    UserId = doctorUser3.Id,
                    SpecialityId = pediatrics.Id,
                    Price = 350m,
                    Bio = "Pediatrician providing healthcare for children and adolescents.",
                    ContactNumber = "01111111113",
                    IsApproved = false,
                    CreatedAt = now
                };

                await context.DoctorProfiles.AddRangeAsync(
                    doctor1,
                    doctor2,
                    doctor3);

                await context.SaveChangesAsync();


                // ============================================================
                // 5. PATIENT PROFILES
                // ============================================================

                var patient1 = new PatientProfile
                {
                    UserId = patientUser1.Id,
                    ContactNumber = "01222222221",
                    DateOfBirth = new DateTime(1999, 5, 15),
                    Address = "Nasr City, Cairo",
                    IsApproved = true,
                    CreatedAt = now
                };

                var patient2 = new PatientProfile
                {
                    UserId = patientUser2.Id,
                    ContactNumber = "01222222222",
                    DateOfBirth = new DateTime(1997, 8, 20),
                    Address = "Dokki, Giza",
                    IsApproved = true,
                    CreatedAt = now
                };

                var patient3 = new PatientProfile
                {
                    UserId = patientUser3.Id,
                    ContactNumber = "01222222223",
                    DateOfBirth = new DateTime(2001, 2, 10),
                    Address = "Maadi, Cairo",
                    IsApproved = true,
                    CreatedAt = now
                };

                await context.PatientProfiles.AddRangeAsync(
                    patient1,
                    patient2,
                    patient3);

                await context.SaveChangesAsync();


                // ============================================================
                // 6. DOCTOR LOCATIONS
                // ============================================================

                var doctorLocation1 = new DoctorLocation
                {
                    DoctorId = doctor1.Id,
                    LocationId = cairoClinic.Id,
                    CreatedAt = now
                };

                var doctorLocation2 = new DoctorLocation
                {
                    DoctorId = doctor1.Id,
                    LocationId = gizaClinic.Id,
                    CreatedAt = now
                };

                var doctorLocation3 = new DoctorLocation
                {
                    DoctorId = doctor2.Id,
                    LocationId = cairoClinic.Id,
                    CreatedAt = now
                };

                var doctorLocation4 = new DoctorLocation
                {
                    DoctorId = doctor2.Id,
                    LocationId = alexClinic.Id,
                    CreatedAt = now
                };

                var doctorLocation5 = new DoctorLocation
                {
                    DoctorId = doctor3.Id,
                    LocationId = gizaClinic.Id,
                    CreatedAt = now
                };

                await context.DoctorLocations.AddRangeAsync(
                    doctorLocation1,
                    doctorLocation2,
                    doctorLocation3,
                    doctorLocation4,
                    doctorLocation5);

                await context.SaveChangesAsync();


                // ============================================================
                // 7. AVAILABILITIES
                // ============================================================

                var availability1 = new Availability
                {
                    DoctorId = doctor1.Id,
                    LocationId = cairoClinic.Id,
                    Date = now.Date.AddDays(1),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(9, 30, 0),
                    IsBooked = true,
                    CreatedAt = now
                };

                var availability2 = new Availability
                {
                    DoctorId = doctor1.Id,
                    LocationId = cairoClinic.Id,
                    Date = now.Date.AddDays(1),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0),
                    IsBooked = false,
                    CreatedAt = now
                };

                var availability3 = new Availability
                {
                    DoctorId = doctor1.Id,
                    LocationId = gizaClinic.Id,
                    Date = now.Date.AddDays(2),
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    IsBooked = true,
                    CreatedAt = now
                };

                var availability4 = new Availability
                {
                    DoctorId = doctor2.Id,
                    LocationId = cairoClinic.Id,
                    Date = now.Date.AddDays(1),
                    StartTime = new TimeSpan(12, 0, 0),
                    EndTime = new TimeSpan(12, 30, 0),
                    IsBooked = true,
                    CreatedAt = now
                };

                var availability5 = new Availability
                {
                    DoctorId = doctor2.Id,
                    LocationId = alexClinic.Id,
                    Date = now.Date.AddDays(2),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(14, 30, 0),
                    IsBooked = false,
                    CreatedAt = now
                };

                var availability6 = new Availability
                {
                    DoctorId = doctor3.Id,
                    LocationId = gizaClinic.Id,
                    Date = now.Date.AddDays(3),
                    StartTime = new TimeSpan(15, 0, 0),
                    EndTime = new TimeSpan(15, 30, 0),
                    IsBooked = false,
                    CreatedAt = now
                };

                await context.Availabilities.AddRangeAsync(
                    availability1,
                    availability2,
                    availability3,
                    availability4,
                    availability5,
                    availability6);

                await context.SaveChangesAsync();


                // ============================================================
                // 8. APPOINTMENTS
                // ============================================================

                var appointment1 = new Appointment
                {
                    PatientId = patient1.Id,
                    DoctorId = doctor1.Id,
                    AvailabilityId = availability1.Id,
                    Status = AppointmentStatus.Confirmed,
                    Notes = "Regular cardiology consultation.",
                    CreatedAt = now
                };

                var appointment2 = new Appointment
                {
                    PatientId = patient2.Id,
                    DoctorId = doctor1.Id,
                    AvailabilityId = availability3.Id,
                    Status = AppointmentStatus.Completed,
                    Notes = "Follow-up appointment.",
                    CreatedAt = now.AddDays(-2)
                };

                var appointment3 = new Appointment
                {
                    PatientId = patient1.Id,
                    DoctorId = doctor2.Id,
                    AvailabilityId = availability4.Id,
                    Status = AppointmentStatus.Pending,
                    Notes = "Skin consultation.",
                    CreatedAt = now
                };

                var appointment4 = new Appointment
                {
                    PatientId = patient2.Id,
                    DoctorId = doctor2.Id,
                    AvailabilityId = availability5.Id,
                    Status = AppointmentStatus.Cancelled,
                    Notes = "Patient cancelled the appointment.",
                    CreatedAt = now.AddDays(-1)
                };

                var appointment5 = new Appointment
                {
                    PatientId = patient3.Id,
                    DoctorId = doctor1.Id,
                    AvailabilityId = availability2.Id,
                    Status = AppointmentStatus.Rejected,
                    Notes = "Appointment rejected by doctor.",
                    CreatedAt = now.AddDays(-1)
                };

                await context.Appointments.AddRangeAsync(
                    appointment1,
                    appointment2,
                    appointment3,
                    appointment4,
                    appointment5);

                await context.SaveChangesAsync();


                // ============================================================
                // 9. PAYMENTS
                // ============================================================

                var payment1 = new Payment
                {
                    PatientId = patient1.Id,
                    AppointmentId = appointment1.Id,
                    PaymentMethod = "Credit Card",
                    Status = PaymentStatus.Completed,
                    TransactionReference = "TXN-100001",
                    PaymentDate = now,
                    Amount = doctor1.Price,
                    CreatedAt = now
                };

                var payment2 = new Payment
                {
                    PatientId = patient2.Id,
                    AppointmentId = appointment2.Id,
                    PaymentMethod = "Cash",
                    Status = PaymentStatus.Completed,
                    TransactionReference = "TXN-100002",
                    PaymentDate = now.AddDays(-2),
                    Amount = doctor1.Price,
                    CreatedAt = now.AddDays(-2)
                };

                var payment3 = new Payment
                {
                    PatientId = patient1.Id,
                    AppointmentId = appointment3.Id,
                    PaymentMethod = "Credit Card",
                    Status = PaymentStatus.Pending,
                    TransactionReference = "TXN-100003",
                    PaymentDate = now,
                    Amount = doctor2.Price,
                    CreatedAt = now
                };

                await context.Payments.AddRangeAsync(
                    payment1,
                    payment2,
                    payment3);

                await context.SaveChangesAsync();


                // ============================================================
                // 10. RATINGS
                // ============================================================

                var rating1 = new Rating
                {
                    DoctorId = doctor1.Id,
                    PatientId = patient2.Id,
                    Rate = 5,
                    Review = "Excellent doctor and very professional.",
                    CreatedAt = now.AddDays(-1)
                };

                var rating2 = new Rating
                {
                    DoctorId = doctor2.Id,
                    PatientId = patient1.Id,
                    Rate = 4,
                    Review = "Very good experience.",
                    CreatedAt = now.AddDays(-1)
                };

                await context.Ratings.AddRangeAsync(
                    rating1,
                    rating2);

                await context.SaveChangesAsync();


                // ============================================================
                // 11. NOTIFICATIONS
                // ============================================================

                var notification1 = new Notification
                {
                    UserId = patientUser1.Id,
                    AppointmentId = appointment1.Id,
                    Message = "Your appointment has been confirmed.",
                    Type = NotificationType.InApp,
                    Status = NotificationStatus.Sent,
                    RetryCount = 0,
                    IsRead = false,
                    SentAt = now,
                    CreatedAt = now
                };

                var notification2 = new Notification
                {
                    UserId = doctorUser1.Id,
                    AppointmentId = appointment3.Id,
                    Message = "You have a new appointment request.",
                    Type = NotificationType.Email,
                    Status = NotificationStatus.Sent,
                    RetryCount = 0,
                    IsRead = false,
                    SentAt = now,
                    CreatedAt = now
                };

                var notification3 = new Notification
                {
                    UserId = patientUser2.Id,
                    AppointmentId = appointment2.Id,
                    Message = "Your appointment has been completed.",
                    Type = NotificationType.Push,
                    Status = NotificationStatus.Sent,
                    RetryCount = 0,
                    IsRead = true,
                    ReadAt = now.AddDays(-1),
                    SentAt = now.AddDays(-1),
                    CreatedAt = now.AddDays(-1)
                };

                var notification4 = new Notification
                {
                    UserId = patientUser1.Id,
                    AppointmentId = null,
                    Message = "Welcome to LeoClinic!",
                    Type = NotificationType.InApp,
                    Status = NotificationStatus.Pending,
                    RetryCount = 0,
                    IsRead = false,
                    SentAt = now,
                    CreatedAt = now
                };

                await context.Notifications.AddRangeAsync(
                    notification1,
                    notification2,
                    notification3,
                    notification4);

                await context.SaveChangesAsync();


                // ============================================================
                // 12. VERIFICATION CODES
                // ============================================================

                var verification1 = new VerificationCode
                {
                    UserId = patientUser1.Id,
                    Type = VerificationType.EmailVerification,
                    Token = "123456",
                    ExpiresAt = now.AddHours(1),
                    UsedAt = null,
                    CreatedAt = now
                };

                var verification2 = new VerificationCode
                {
                    UserId = patientUser2.Id,
                    Type = VerificationType.PasswordReset,
                    Token = "654321",
                    ExpiresAt = now.AddHours(1),
                    UsedAt = null,
                    CreatedAt = now
                };

                await context.VerificationCodes.AddRangeAsync(
                    verification1,
                    verification2);

                await context.SaveChangesAsync();


                // ============================================================
                // 13. REFRESH TOKENS
                // ============================================================

                var refreshToken1 = new RefreshToken
                {
                    UserId = patientUser1.Id,
                    Token = Guid.NewGuid().ToString("N"),
                    ExpiresAt = now.AddDays(7),
                    RevokedAt = null,
                    CreatedAt = now
                };

                var refreshToken2 = new RefreshToken
                {
                    UserId = doctorUser1.Id,
                    Token = Guid.NewGuid().ToString("N"),
                    ExpiresAt = now.AddDays(7),
                    RevokedAt = null,
                    CreatedAt = now
                };

                await context.RefreshTokens.AddRangeAsync(
                    refreshToken1,
                    refreshToken2);

                await context.SaveChangesAsync();


                // ============================================================
                // COMMIT
                // ============================================================

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}