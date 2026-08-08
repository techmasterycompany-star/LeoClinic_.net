using FluentValidation;
using LeoClinic.Application.Interfaces;
using LeoClinic.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LeoClinic.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddScoped<IAdminService, AdminService>();
            return services;
        }
    }
}
