using FluentValidation;
using LeoClinic.Application.DTOs;
using LeoClinic.Application.Interfaces;
using LeoClinic.Application.Services;
using Microsoft.Extensions.Configuration;
using LeoClinic.Application.Interfaces;
using LeoClinic.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LeoClinic.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IAdminService, AdminService>();
            return services;
        }
    }
}
