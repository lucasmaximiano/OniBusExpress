using OnibusExpress.Domain.Entities;

namespace OnibusExpress.Domain.Interfaces
{
    public interface IPassageirosRepository
    {
        Task CriarAsync(
            Passageiro passageiro,
            CancellationToken cancellationToken);

        Task<Passageiro?> ObterPorIdAsync(
          Guid id,
          CancellationToken cancellationToken);

        Task<Passageiro?> AtualizarAsync(
            Guid id,
            Passageiro request,
            CancellationToken cancellationToken);
    }
}
