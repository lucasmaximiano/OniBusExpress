using FluentValidation;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Interfaces;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class RotasApplication(
        IValidator<CreateRotaRequest> createValidator,
        IRotasRepository rotasRepository) : IRotasApplication
    {
        private readonly IValidator<CreateRotaRequest> _createValidator = createValidator;
        private readonly IRotasRepository _rotasRepository = rotasRepository;

        public async Task CriarAsync(
             CreateRotaRequest request,
             CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(
                request,
                cancellationToken);

            Rota rota = new(
                            request.Origem, 
                            request.Destino, 
                            request.DuracaoEstimada);

            await _rotasRepository.CriarAsync(
                      rota,
                      cancellationToken);
        }

        public async Task<RotaResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            Rota? rota = 
                await _rotasRepository.ObterPorIdAsync(id, cancellationToken);

            if (rota is null)
                return null;

            return new RotaResponse(
                rota.Id, 
                rota.Origem, 
                rota.Destino, 
                rota.DuracaoEstimada);
        }
    }
}
