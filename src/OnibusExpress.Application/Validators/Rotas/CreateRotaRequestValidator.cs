using FluentValidation;
using OnibusExpress.Application.DTOs.Rotas;

namespace OnibusExpress.Application.Validators.Rotas
{
    public class CreateRotaRequestValidator
    : AbstractValidator<CreateRotaRequest>
    {
        public CreateRotaRequestValidator()
        {
            RuleFor(x => x.Origem)
                .NotEmpty()
                .WithMessage("A origem é obrigatória.")
                .MaximumLength(150)
                .WithMessage("A origem deve possuir no máximo 150 caracteres.");

            RuleFor(x => x.Destino)
                .NotEmpty()
                .WithMessage("O destino é obrigatório.")
                .MaximumLength(150)
                .WithMessage("O destino deve possuir no máximo 150 caracteres.");

            RuleFor(x => x.DuracaoEstimada)
                .GreaterThan(TimeSpan.Zero)
                .WithMessage("A duração estimada deve ser maior que zero.");
        }
    }
}