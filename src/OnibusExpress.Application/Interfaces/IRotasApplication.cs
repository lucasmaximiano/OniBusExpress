using OnibusExpress.Application.DTOs.Rotas;

namespace OnibusExpress.Application.Interfaces
{

    public interface IRotasApplication
    {
        Task CriarAsync(
            CreateRotaRequest request,
            CancellationToken cancellationToken);

        Task<RotaResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<IEnumerable<RotaResponse>> ObterTodasAsync(
            CancellationToken cancellationToken);

        Task<RotaResponse> AtualizarAsync(
            Guid id,
            UpdateRotaRequest request,
            CancellationToken cancellationToken);

        Task ExcluirAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
