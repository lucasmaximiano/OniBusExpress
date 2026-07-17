using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;
using OnibusExpress.Infrastructure.Context;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class PassageirosRepository(OnibusExpressContext context) : IPassageirosRepository
    {
        private readonly OnibusExpressContext _context = context;

        public async Task CriarAsync(
            Passageiro passageiro,
            CancellationToken cancellationToken)
        {
            await _context.Passageiros.AddAsync(
                passageiro,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Passageiro?> ObterPorIdAsync(
             Guid id,
             CancellationToken cancellationToken)
        {
            return await _context.Passageiros
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Passageiro?> AtualizarAsync(
             Passageiro request,
             CancellationToken cancellationToken)
        {
            _context.Passageiros.Update(request);

            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}
