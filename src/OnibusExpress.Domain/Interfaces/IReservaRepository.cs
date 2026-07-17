using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task CriarAsync(
          Reserva request,
          CancellationToken cancellationToken);

        Task<Reserva?> ObterPorCodigoAsync(
           string codigoReserva,
           CancellationToken cancellationToken);

        Task<Reserva?> ObterReservaPorViagemEAssentoAsync(
            Guid viagemId,
            int numeroAssento,
            CancellationToken cancellationToken);

        Task<IEnumerable<Reserva>> ObterTodasAsync(
            CancellationToken cancellationToken);

        Task CancelarAsync(
            Reserva reserva,
            CancellationToken cancellationToken);
    }
}
