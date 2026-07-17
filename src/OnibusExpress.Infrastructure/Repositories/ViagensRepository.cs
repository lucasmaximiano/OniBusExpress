using Microsoft.EntityFrameworkCore;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;
using OnibusExpress.Infrastructure.Context;

namespace OnibusExpress.Infrastructure.Repositories
{
    public class ViagensRepository(OnibusExpressContext context) : IViagensRepository
    {
        private readonly OnibusExpressContext _context = context;

        public async Task CriarAsync(
            Viagem request,
            CancellationToken cancellationToken)
        {
            await _context.Viagens.AddAsync(
                request,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Viagem?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Viagens
                .Include(x => x.Rota)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IEnumerable<Viagem>> ObterPorFiltroAsync(
            string origem,
            string destino,
            CancellationToken cancellationToken)
        {
            IQueryable<Viagem> query = _context.Viagens
                  .AsNoTracking()
                  .Include(x => x.Rota);

            if (!string.IsNullOrEmpty(origem))
                query = query.Where(x =>
                    x.Rota.Origem.Contains(origem));

            if (!string.IsNullOrEmpty(destino))
                query = query.Where(x =>
                    x.Rota.Destino.Contains(destino));

            return await query.ToListAsync(cancellationToken);
        }
    }
}