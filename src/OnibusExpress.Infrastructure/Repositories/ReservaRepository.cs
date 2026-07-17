using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        public Task CriarAsync(Reserva request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Reserva?> ObterPorCodigoAsync(string codigoReserva, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reserva>> ObterTodasAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CancelarAsync(string codigoReserva, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
