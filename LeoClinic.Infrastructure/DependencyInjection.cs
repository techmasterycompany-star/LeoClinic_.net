using LeoClinic.Application.Interfaces;
<<<<<<< HEAD
using LeoClinic.Application.Services;
using LeoClinic.Infrastructure.Data;
using LeoClinic.Infrastructure.Repositories;
using LeoClinic.Infrastructure.Services;
=======
using LeoClinic.Infrastructure.Data;
using LeoClinic.Infrastructure.Repositories;
>>>>>>> 58cd926 (Implement admin management features)
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeoClinic.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

<<<<<<< HEAD
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.AddScoped<IPaymentGateway, StripePaymentGateway>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IAvailabilityRepository, AvailabilitytRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

=======
            services.AddScoped<IAdminRepository, AdminRepository>(); 
>>>>>>> 58cd926 (Implement admin management features)
            return services;
        }
    }
}
