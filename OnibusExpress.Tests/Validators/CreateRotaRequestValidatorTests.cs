using FluentValidation.TestHelper;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Validators.Rotas;

namespace OnibusExpress.Tests.Application.Validators
{

    public class CreateRotaRequestValidatorTests
    {
        private readonly CreateRotaRequestValidator _validator = new();

        [Fact]
        public void Validate_DeveRetornarErro_QuandoOrigemForVazia()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Origem = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Origem)
                .WithErrorMessage("A origem é obrigatória.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoOrigemPossuirMaisDe150Caracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Origem = new string('A', 151)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Origem)
                .WithErrorMessage(
                    "A origem deve possuir no máximo 150 caracteres.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDestinoForVazio()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Destino = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Destino)
                .WithErrorMessage("O destino é obrigatório.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoDestinoPossuirMaisDe150Caracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Destino = new string('A', 151)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Destino)
                .WithErrorMessage(
                    "O destino deve possuir no máximo 150 caracteres.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Validate_DeveRetornarErro_QuandoDuracaoEstimadaNaoForMaiorQueZero(
            int minutos)
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                DuracaoEstimada = TimeSpan.FromMinutes(minutos)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DuracaoEstimada)
                .WithErrorMessage(
                    "A duração estimada deve ser maior que zero.");
        }

        [Fact]
        public void Validate_NaoDeveRetornarErro_QuandoOrigemPossuirExatamente150Caracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Origem = new string('A', 150)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Origem);
        }

        [Fact]
        public void Validate_NaoDeveRetornarErro_QuandoDestinoPossuirExatamente150Caracteres()
        {
            // Arrange
            var request = CriarRequestValido() with
            {
                Destino = new string('A', 150)
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Destino);
        }

        [Fact]
        public void Validate_DeveRetornarTodosOsErros_QuandoRequestForInvalido()
        {
            // Arrange
            var request = new CreateRotaRequest(
                string.Empty,
                string.Empty,
                TimeSpan.Zero);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Origem)
                .WithErrorMessage("A origem é obrigatória.");

            result.ShouldHaveValidationErrorFor(x => x.Destino)
                .WithErrorMessage("O destino é obrigatório.");

            result.ShouldHaveValidationErrorFor(x => x.DuracaoEstimada)
                .WithErrorMessage(
                    "A duração estimada deve ser maior que zero.");

            Assert.Equal(3, result.Errors.Count);
        }

        [Fact]
        public void Validate_NaoDeveRetornarErros_QuandoRequestForValido()
        {
            // Arrange
            var request = CriarRequestValido();

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        private static CreateRotaRequest CriarRequestValido()
        {
            return new CreateRotaRequest(
                Origem: "São Paulo",
                Destino: "Campinas",
                DuracaoEstimada: TimeSpan.FromHours(2));
        }
    }
}