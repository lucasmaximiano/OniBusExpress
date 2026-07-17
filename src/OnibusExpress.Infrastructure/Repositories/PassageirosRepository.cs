using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class PassageirosRepository : IPassageirosRepository
    {
        public Task CriarAsync(Passageiro passageiro, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Passageiro?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Passageiro?> AtualizarAsync(Guid id, Passageiro request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
    }
}
