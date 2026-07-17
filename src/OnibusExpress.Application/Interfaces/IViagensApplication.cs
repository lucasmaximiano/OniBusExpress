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

        Task<ViagemResponse?> ObterPorFiltro(
            string viagem,
            string destino,
            CancellationToken cancellationToken);
    }
}
