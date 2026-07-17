using FluentValidation;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Interfaces;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class ViagensApplication(
        IValidator<CreateViagemRequest> createValidator,
        IViagensRepository viagensRepository) : IViagensApplication
    {
        private readonly IValidator<CreateViagemRequest> _createValidator = createValidator;
        private readonly IViagensRepository _viagensRepository = viagensRepository;

       public async Task CriarAsync(
            CreateViagemRequest request, 
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(
                   request,
                   cancellationToken);

            Viagem viagem = new(
                    request.RotaId, 
                    request.DataHoraPartida, 
                    request.PrecoBase, 
                    request.AssentosDisponiveis);

            await _viagensRepository.CriarAsync(viagem, cancellationToken);
        }

        public async Task<ViagemResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            Viagem? viagem = await _viagensRepository.ObterPorIdAsync(
                id,
                cancellationToken);

            return viagem is null
                ? null
                : MapToResponse(viagem);
        }

        public async Task<IEnumerable<ViagemResponse>> ObterPorFiltroAsync(
            string origem,
            string destino,
            CancellationToken cancellationToken)
        {
            IEnumerable<Viagem> viagens = await _viagensRepository.ObterPorFiltroAsync(
                origem,
                destino,
                cancellationToken);

            return viagens.Select(MapToResponse);
        }

        private static ViagemResponse MapToResponse(Viagem viagem)
        {
            return new ViagemResponse(
                viagem.Id,
                viagem.RotaId,
                viagem.DataHoraPartida,
                viagem.PrecoBase,
                viagem.AssentosDisponiveis,
                new RotaResponse(
                    viagem.Rota.Id,
                    viagem.Rota.Origem,
                    viagem.Rota.Destino,
                    viagem.Rota.DuracaoEstimada
                )
            );
        }
    }
}
