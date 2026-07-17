using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Domain.Interfaces
{
    public interface IViagensRepository
    {
        Task CriarAsync(
         Viagem request,
         CancellationToken cancellationToken);

        Task<Viagem?> ObterPorIdAsync(
           Guid id,
           CancellationToken cancellationToken);

        Task<IEnumerable<Viagem>> ObterPorFiltroAsync(
            string viagem,
            string destino,
            CancellationToken cancellationToken);
    }
}
