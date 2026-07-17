using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;
using OnibusExpress.Infrastructure.Context;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class RotasRepository(OnibusExpressContext context) : IRotasRepository
    {
        private readonly OnibusExpressContext _context = context;

        public async Task CriarAsync(
            Rota request,
            CancellationToken cancellationToken)
        {
            await _context.Rotas.AddAsync(
                request,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Rota?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Rotas
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}