using FluentValidation;
using OnibusExpress.Application.DTOs.Passageiros;

namespace OnibusExpress.Application.Validators.Passageiros
{
    public class UpdatePassageiroRequestValidator
     : AbstractValidator<UpdatePassageiroRequest>
    {
        public UpdatePassageiroRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve possuir pelo menos 3 caracteres.")
                .MaximumLength(150)
                .WithMessage("O nome deve possuir no máximo 150 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido.")
                .MaximumLength(200)
                .WithMessage("O e-mail deve possuir no máximo 200 caracteres.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.");
        }
    }
}
