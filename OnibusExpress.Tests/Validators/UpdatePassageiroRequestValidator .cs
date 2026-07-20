using FluentValidation.TestHelper;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.Validators.Passageiros;

namespace OnibusExpress.Tests.Application.Validators
{

    public class UpdatePassageiroRequestValidatorTests
    {
        private readonly UpdatePassageiroRequestValidator _validator = new();

        [Fact]
        public void Validate_DeveRetornarErro_QuandoNomeForVazio()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Nome = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("O nome é obrigatório.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoNomePossuirMenosDeTresCaracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Nome = "Lu"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage(
                    "O nome deve possuir pelo menos 3 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoNomePossuirMaisDeCentoECinquentaCaracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Nome = new string('A', 151)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage(
                    "O nome deve possuir no máximo 150 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoEmailForVazio()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Email = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("O e-mail é obrigatório.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoEmailForInvalido()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Email = "email-invalido"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("O e-mail informado é inválido.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDataNascimentoForVazia()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                DataNascimento = default
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
                .WithErrorMessage(
                    "A data de nascimento é obrigatória.");
        }

        [Fact]
        public void Validate_NaoDeveRetornarErro_QuandoRequestForValido()
        {
            // Arrange
            var request = CriarRequestValido();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        private static UpdatePassageiroRequest CriarRequestValido()
        {
            return new UpdatePassageiroRequest(
                Nome: "Lucas Maximiano",
                Email: "lucas@email.com",
                DataNascimento: new DateTime(1990, 5, 10));
        }
    }
}