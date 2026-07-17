using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Infrastructure.Context
{
    public class OnibusExpressContext : DbContext
    {
        public OnibusExpressContext(
            DbContextOptions<OnibusExpressContext> options)
            : base(options)
        {
        }

        public DbSet<Rota> Rotas => Set<Rota>();
        public DbSet<Viagem> Viagens => Set<Viagem>();
        public DbSet<Passageiro> Passageiros => Set<Passageiro>();
        public DbSet<Reserva> Reservas => Set<Reserva>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OnibusExpressContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}