using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnibusExpress.Domain.Interfaces;
using OnibusExpress.Infrastructure.Context;
using OnibusExpress.Infrastructure.Repositories;

namespace OnibusExpress.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OnibusExpressContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRotasRepository, RotasRepository>();
            services.AddScoped<IViagensRepository, ViagensRepository>();
            services.AddScoped<IPassageirosRepository, PassageirosRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            return services;
        }
    }
}