using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Interfaces;
using OnibusExpress.Application.Service;
using OnibusExpress.Application.Validators.Passageiros;
using OnibusExpress.Application.Validators.Reserva;
using OnibusExpress.Application.Validators.Rotas;
using OnibusExpress.Application.Validators.Viagens;

namespace OnibusExpress.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            RegisterApplications(services);
            RegisterValidators(services);

            return services;
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services.AddScoped<IRotasApplication, RotasApplication>();
            services.AddScoped<IViagensApplication, ViagensApplication>();
            services.AddScoped<IPassageirosApplication, PassageirosApplication>();
            services.AddScoped<IReservasApplication, ReservasApplication>();
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<
                IValidator<CreateRotaRequest>,
                CreateRotaRequestValidator>();

            services.AddScoped<
                IValidator<CreateViagemRequest>,
                CreateViagemRequestValidator>();

            services.AddScoped<
                IValidator<CreatePassageiroRequest>,
                CreatePassageiroRequestValidator>();

            services.AddScoped<
                IValidator<UpdatePassageiroRequest>,
                UpdatePassageiroRequestValidator>();

            services.AddScoped<
                IValidator<CreateReservaRequest>,
                CreateReservaRequestValidator>();

            return;
        }
    }
}
