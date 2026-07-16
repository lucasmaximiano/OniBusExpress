using FluentValidation;
using OnibusExpress.Application.DTOs.Viagens;

namespace OnibusExpress.Application.Validators.Viagens
{

    public sealed class UpdateViagemRequestValidator
        : AbstractValidator<UpdateViagemRequest>
    {
        public UpdateViagemRequestValidator()
        {
            RuleFor(x => x.RotaId)
                .NotEmpty()
                .WithMessage("A rota é obrigatória.");

            RuleFor(x => x.DataHoraPartida)
                .NotEmpty()
                .WithMessage("A data e a hora de partida são obrigatórias.");

            RuleFor(x => x.PrecoBase)
                .GreaterThan(0)
                .WithMessage("O preço-base deve ser maior que zero.")
                .PrecisionScale(10, 2, false)
                .WithMessage("O preço-base deve possuir no máximo duas casas decimais.");
        }
    }
}
