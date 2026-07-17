using OnibusExpress.Application.DTOs.Passageiros;

namespace OnibusExpress.Application.Interfaces
{
    public interface IPassageirosApplication
    {
        Task CriarAsync(
          CreatePassageiroRequest request,
          CancellationToken cancellationToken);

        Task<PassageiroResponse?> ObterPorIdAsync(
          Guid id,
          CancellationToken cancellationToken);

        Task<PassageiroResponse?> AtualizarAsync(
            Guid id,
            UpdatePassageiroRequest request,
            CancellationToken cancellationToken);
    }
}
