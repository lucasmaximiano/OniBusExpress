using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Application.Service
{
    internal class ReservasApplication : IReservasApplication
    {
        public Task CancelarAsync(string codigoReserva, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CriarAsync(CreateReservaRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ReservaResponse?> ObterPorCodigoAsync(string codigoReserva, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservaResponse>> ObterTodasAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
