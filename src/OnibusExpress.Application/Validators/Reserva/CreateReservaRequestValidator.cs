using FluentValidation;
using OnibusExpress.Application.DTOs.Reserva;

namespace OnibusExpress.Application.Validators.Reserva
{
    public class CreateReservaRequestValidator
      : AbstractValidator<CreateReservaRequest>
    {
        public CreateReservaRequestValidator()
        {
            RuleFor(x => x.ViagemId)
                .NotEmpty()
                .WithMessage("A viagem é obrigatória.");

            RuleFor(x => x.PassageiroId)
                .NotEmpty()
                .WithMessage("O passageiro é obrigatório.");

            RuleFor(x => x.NumeroAssento)
                .GreaterThan(0)
                .WithMessage("O número do assento deve ser maior que zero.");
        }
    }
}
