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
            string viagem,
            string destino,
            CancellationToken cancellationToken)
        {
            return await _context.Viagens
                .Include(x => x.Rota)
                .Where(x =>
                    (string.IsNullOrWhiteSpace(viagem) ||
                     x.Rota.Origem.Contains(viagem)) &&
                    (string.IsNullOrWhiteSpace(destino) ||
                     x.Rota.Destino.Contains(destino)))
                .ToListAsync(cancellationToken);
        }
    }
}