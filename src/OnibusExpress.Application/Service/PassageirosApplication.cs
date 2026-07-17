using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Application.Service
{
    internal class PassageirosApplication : IPassageirosApplication
    {
        public Task<PassageiroResponse?> AtualizarAsync(Guid id, UpdatePassageiroRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CriarAsync(CreatePassageiroRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PassageiroResponse?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
