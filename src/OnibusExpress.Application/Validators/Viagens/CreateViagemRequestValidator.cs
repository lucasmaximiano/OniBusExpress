using FluentValidation;
using OnibusExpress.Application.DTOs.Viagens;

namespace OnibusExpress.Application.Validators.Viagens
{
    public class CreateViagemRequestValidator
     : AbstractValidator<CreateViagemRequest>
    {
        public CreateViagemRequestValidator()
        {
            RuleFor(x => x.RotaId)
                .NotEmpty()
                .WithMessage("A rota é obrigatória.");

            RuleFor(x => x.DataHoraPartida)
                .NotEmpty()
                .WithMessage("A data e a hora de partida são obrigatórias.")
                .GreaterThan(DateTime.Now)
                .WithMessage("A data e a hora de partida devem estar no futuro.");

            RuleFor(x => x.PrecoBase)
                .GreaterThan(0)
                .WithMessage("O preço-base deve ser maior que zero.");
        }
    }
}
