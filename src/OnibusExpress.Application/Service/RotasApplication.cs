using FluentValidation;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class RotasApplication(
        IValidator<CreateRotaRequest> createValidator) : IRotasApplication
    {
        private readonly IValidator<CreateRotaRequest> _createValidator = createValidator;

        public async Task CriarAsync(
            CreateRotaRequest request,
            CancellationToken cancellationToken = default)
        {
            await _createValidator.ValidateAndThrowAsync(
                request,
                cancellationToken);
        }

        public Task<RotaResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<RotaResponse?>(null);
        }
    }
}
