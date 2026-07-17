using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class RotasRepository : IRotasRepository
    {
        public Task CriarAsync(Rota request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Rota?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
