using FluentValidation;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Application
{
    public sealed class RotasApplication : IRotasApplication
    {
        private readonly IValidator<CreateRotaRequest> _createValidator;
        private readonly IValidator<UpdateRotaRequest> _updateValidator;

        public RotasApplication(
            IValidator<CreateRotaRequest> createValidator,
            IValidator<UpdateRotaRequest> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<RotaResponse> CriarAsync(
            CreateRotaRequest request,
            CancellationToken cancellationToken = default)
        {
            await _createValidator.ValidateAndThrowAsync(
                request,
                cancellationToken);

            return new RotaResponse(
                Guid.NewGuid(),
                request.Origem.Trim(),
                request.Destino.Trim(),
                request.DuracaoEstimada);
        }

        public Task<RotaResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<RotaResponse?>(null);
        }

        public Task<IReadOnlyCollection<RotaResponse>> ObterTodasAsync(
            CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<RotaResponse> rotas = [];

            return Task.FromResult(rotas);
        }

        public async Task<RotaResponse> AtualizarAsync(
            Guid id,
            UpdateRotaRequest request,
            CancellationToken cancellationToken = default)
        {
            await _updateValidator.ValidateAndThrowAsync(
                request,
                cancellationToken);

            return new RotaResponse(
                id,
                request.Origem.Trim(),
                request.Destino.Trim(),
                request.DuracaoEstimada);
        }

        public Task ExcluirAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
