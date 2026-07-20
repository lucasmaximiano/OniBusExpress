using FluentValidation.TestHelper;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Validators.Viagens;

namespace OnibusExpress.Tests.Application.Validators.Viagens;

public class CreateViagemRequestValidatorTests
{
    private readonly CreateViagemRequestValidator _validator = new();

    [Fact]
    public void Validate_DeveRetornarErro_QuandoRotaIdForVazio()
    {
        // Arrange
        var request = CriarRequestValido() with
        {
            RotaId = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.RotaId)
            .WithErrorMessage("A rota é obrigatória.");
    }

    [Fact]
    public void Validate_DeveRetornarErro_QuandoDataHoraPartidaForPadrao()
    {
        // Arrange
        var request = CriarRequestValido() with
        {
            DataHoraPartida = default
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DataHoraPartida)
            .WithErrorMessage("A data e a hora de partida são obrigatórias.");
    }

    [Fact]
    public void Validate_DeveRetornarErro_QuandoDataHoraPartidaEstiverNoPassado()
    {
        // Arrange
        var request = CriarRequestValido() with
        {
            DataHoraPartida = DateTime.Now.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DataHoraPartida)
            .WithErrorMessage("A data e a hora de partida devem estar no futuro.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Validate_DeveRetornarErro_QuandoPrecoBaseNaoForMaiorQueZero(
        decimal precoBase)
    {
        // Arrange
        var request = CriarRequestValido() with
        {
            PrecoBase = precoBase
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PrecoBase)
            .WithErrorMessage("O preço-base deve ser maior que zero.");
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

    private static CreateViagemRequest CriarRequestValido()
    {
        return new CreateViagemRequest(
            RotaId: Guid.NewGuid(),
            DataHoraPartida: DateTime.Now.AddHours(2),
            PrecoBase: 150.00m,
            AssentosDisponiveis: 2);
    }
}