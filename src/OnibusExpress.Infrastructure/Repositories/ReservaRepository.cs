using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;
using OnibusExpress.Infrastructure.Context;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class ReservaRepository(OnibusExpressContext context) : IReservaRepository
    {
        private readonly OnibusExpressContext _context = context;

        public async Task CriarAsync(
            Reserva request,
            CancellationToken cancellationToken)
        {
            await _context.Reservas.AddAsync(
                request,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Reserva?> ObterPorCodigoAsync(
            string codigoReserva,
            CancellationToken cancellationToken)
        {
            return await _context.Reservas
                .Include(x => x.Viagem)
                    .ThenInclude(x => x.Rota)
                .Include(x => x.Passageiro)
                .FirstOrDefaultAsync(
                    x => x.CodigoReserva == codigoReserva,
                    cancellationToken);
        }

        public async Task<IEnumerable<Reserva>> ObterTodasAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Reservas
                .Include(x => x.Viagem)
                    .ThenInclude(x => x.Rota)
                .Include(x => x.Passageiro)
                .ToListAsync(cancellationToken);
        }

        public async Task<Reserva?> ObterReservaPorViagemEAssentoAsync(
            Guid viagemId,
            int numeroAssento,
            CancellationToken cancellationToken)
        {
            return await _context.Reservas
                .Include(x => x.Viagem)
                .Include(x => x.Passageiro)
                .FirstOrDefaultAsync(
                    x => x.ViagemId == viagemId &&
                         x.NumeroAssento == numeroAssento,
                    cancellationToken);
        }

        public async Task CancelarAsync(
            Reserva reserva,
            CancellationToken cancellationToken)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}