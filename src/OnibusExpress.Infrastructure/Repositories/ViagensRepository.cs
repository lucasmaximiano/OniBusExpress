using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class ViagensRepository : IViagensRepository
    {
        public Task CriarAsync(Viagem request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Viagem?> ObterPorFiltro(string viagem, string destino, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Viagem?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
