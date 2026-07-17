using FluentValidation;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.Interfaces;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class PassageirosApplication(
          IValidator<CreatePassageiroRequest> createValidator,
          IValidator<UpdatePassageiroRequest> updateValidator,
          IPassageirosRepository passageirosRepository) : IPassageirosApplication
    {
        private readonly IValidator<CreatePassageiroRequest> _createValidator = createValidator;
        private readonly IValidator<UpdatePassageiroRequest> _updateValidator = updateValidator;
        private readonly IPassageirosRepository _passageirosRepository = passageirosRepository;

        public async Task CriarAsync(
            CreatePassageiroRequest request,
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(
               request,
               cancellationToken);

            Passageiro passageiro = new(
                request.Nome,
                request.Cpf,
                request.Email,
                request.DataNascimento);

            await _passageirosRepository.CriarAsync(
                passageiro,
                cancellationToken);

        }

        public async Task<PassageiroResponse?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            Passageiro? passageiro = await _passageirosRepository.ObterPorIdAsync(
                                                                id,
                                                                cancellationToken);
            if (passageiro is null)
                return null;

            return new PassageiroResponse(
                passageiro.Id,
                passageiro.Nome,
                passageiro.Cpf,
                passageiro.Email,
                passageiro.DataNascimento);

        }

        public async Task<PassageiroResponse?> AtualizarAsync(
            Guid id,
            UpdatePassageiroRequest request,
            CancellationToken cancellationToken)
        {
            await _updateValidator.ValidateAndThrowAsync(
              request,
              cancellationToken);

            Passageiro? passageiro = await _passageirosRepository.ObterPorIdAsync(
                                                            id,
                                                            cancellationToken);
            if (passageiro is null)
                return null;

            passageiro.Atualizar(
                                request.Nome,
                                request.Email,
                                request.DataNascimento);

            await _passageirosRepository.AtualizarAsync(
                                                        id,
                                                        passageiro,
                                                        cancellationToken);

            return new PassageiroResponse(
               passageiro.Id,
               passageiro.Nome,
               passageiro.Cpf,
               passageiro.Email,
               passageiro.DataNascimento);
        }
    }
}
