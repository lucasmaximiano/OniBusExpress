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
    }
}
