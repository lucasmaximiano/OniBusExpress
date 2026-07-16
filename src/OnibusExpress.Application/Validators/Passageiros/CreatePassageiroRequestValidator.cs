using FluentValidation;
using OnibusExpress.Application.Common;
using OnibusExpress.Application.DTOs.Passageiros;

namespace OnibusExpress.Application.Validators.Passageiros
{
    public class CreatePassageiroRequestValidator
    : AbstractValidator<CreatePassageiroRequest>
    {
        public CreatePassageiroRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve possuir pelo menos 3 caracteres.");

            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .Must(CpfValidator.IsValid)
                .WithMessage("O CPF informado é inválido.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.");
        }
    }
}
