using FluentValidation.TestHelper;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.Validators.Reserva;

namespace OnibusExpress.Tests.Application.Validators
{

    public class CreateReservaRequestValidatorTests
    {
        private readonly CreateReservaRequestValidator _validator = new();

        [Fact]
        public void Validate_DeveRetornarErro_QuandoViagemIdForVazio()
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.Empty,
                Guid.NewGuid(),
                1);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ViagemId)
                .WithErrorMessage("A viagem é obrigatória.");
        }

        [Fact]
        public void Validate_DeveRetornarErro_QuandoPassageiroIdForVazio()
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.NewGuid(),
                Guid.Empty,
                1);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PassageiroId)
                .WithErrorMessage("O passageiro é obrigatório.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Validate_DeveRetornarErro_QuandoNumeroAssentoNaoForMaiorQueZero(
            int numeroAssento)
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.NewGuid(),
                Guid.NewGuid(),
                numeroAssento);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NumeroAssento)
                .WithErrorMessage(
                    "O número do assento deve ser maior que zero.");
        }

        [Fact]
        public void Validate_NaoDeveRetornarErros_QuandoRequestForValido()
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.NewGuid(),
                Guid.NewGuid(),
                15);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_DeveRetornarTodosOsErros_QuandoRequestForInvalido()
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.Empty,
                Guid.Empty,
                0);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ViagemId)
                .WithErrorMessage("A viagem é obrigatória.");

            result.ShouldHaveValidationErrorFor(x => x.PassageiroId)
                .WithErrorMessage("O passageiro é obrigatório.");

            result.ShouldHaveValidationErrorFor(x => x.NumeroAssento)
                .WithErrorMessage(
                    "O número do assento deve ser maior que zero.");

            Assert.Equal(3, result.Errors.Count);
        }
    }
}