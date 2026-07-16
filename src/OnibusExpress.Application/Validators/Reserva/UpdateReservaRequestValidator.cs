using FluentValidation;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Application.Validators.Reserva
{
    public sealed class UpdateReservaRequestValidator
     : AbstractValidator<UpdateReservaRequest>
    {
        public UpdateReservaRequestValidator()
        {
            RuleFor(x => x.NumeroAssento)
                .GreaterThan(0)
                .WithMessage("O número do assento deve ser maior que zero.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("O status informado é inválido.");

        }
    }
}
