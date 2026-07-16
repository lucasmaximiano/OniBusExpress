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

        Task<IEnumerable<ViagemResponse>> ObterTodasAsync(
           CancellationToken cancellationToken );

        Task<ViagemResponse?> AtualizarAsync(
            Guid id,
            UpdateViagemRequest request,
            CancellationToken cancellationToken = default);

        Task ExcluirAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
