using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class ViagensApplication : IViagensApplication
    {
        public Task CriarAsync(
            CreateViagemRequest request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ViagemResponse?> ObterPorFiltro(
            string viagem, 
            string destino, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ViagemResponse?> ObterPorIdAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
