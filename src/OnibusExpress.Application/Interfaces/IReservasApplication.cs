using OnibusExpress.Application.DTOs.Reserva;

namespace OnibusExpress.Application.Interfaces
{
    public interface IReservasApplication
    {
        Task CriarAsync(
            CreateReservaRequest request,
            CancellationToken cancellationToken);

        Task<ReservaResponse?> ObterPorCodigoAsync(
           string codigoReserva,
           CancellationToken cancellationToken);

        Task CancelarAsync(
            string codigoReserva,
            CancellationToken cancellationToken);
    }
}
