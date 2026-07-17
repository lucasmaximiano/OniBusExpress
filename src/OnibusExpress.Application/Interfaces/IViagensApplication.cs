using OnibusExpress.Application.DTOs.Viagens;

namespace OnibusExpress.Application.Interfaces
{
    public interface IViagensApplication
    {
        Task CriarAsync(
           CreateViagemRequest request,
           CancellationToken cancellationToken);

        Task<ViagemResponse?> ObterPorIdAsync(
           Guid id,
           CancellationToken cancellationToken);

        Task<IEnumerable<ViagemResponse>> ObterPorFiltroAsync(
            string origem,
            string destino,
            CancellationToken cancellationToken);
    }
}
