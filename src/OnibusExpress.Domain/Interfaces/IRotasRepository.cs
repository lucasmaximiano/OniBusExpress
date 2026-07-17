using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Domain.Interfaces
{
    public interface IRotasRepository
    {
        Task CriarAsync(
          Rota request,
          CancellationToken cancellationToken);

        Task<Rota?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
