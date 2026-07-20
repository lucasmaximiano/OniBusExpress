using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Service;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Tests.Application;

public class RotasApplicationTests
{
    private readonly Mock<IValidator<CreateRotaRequest>> _createValidatorMock;
    private readonly Mock<IRotasRepository> _rotasRepositoryMock;
    private readonly RotasApplication _application;

    public RotasApplicationTests()
    {
        _createValidatorMock =
            new Mock<IValidator<CreateRotaRequest>>();

        _rotasRepositoryMock =
            new Mock<IRotasRepository>();

        _application = new RotasApplication(
            _createValidatorMock.Object,
            _rotasRepositoryMock.Object);
    }

    [Fact]
    public async Task CriarAsync_DeveCriarRota_QuandoRequestForValido()
    {
        // Arrange
        var request = new CreateRotaRequest("São Paulo", "Rio de Janeiro", TimeSpan.FromHours(6));

        _createValidatorMock
            .Setup(validator => validator.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _rotasRepositoryMock
            .Setup(repository => repository.CriarAsync(
                It.IsAny<Rota>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _application.CriarAsync(
            request,
            CancellationToken.None);

        // Assert
        _createValidatorMock.Verify(
            validator => validator.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _rotasRepositoryMock.Verify(
            repository => repository.CriarAsync(
                It.Is<Rota>(rota =>
                    rota.Origem == request.Origem &&
                    rota.Destino == request.Destino &&
                    rota.DuracaoEstimada == request.DuracaoEstimada),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarValidationException_QuandoRequestForInvalido()
    {
        // Arrange
        var request = new CreateRotaRequest(string.Empty, string.Empty, TimeSpan.Zero);

        var validationFailures = new List<ValidationFailure>
        {
            new(
                nameof(CreateRotaRequest.Origem),
                "A origem é obrigatória."),

            new(
                nameof(CreateRotaRequest.Destino),
                "O destino é obrigatório."),

            new(
                nameof(CreateRotaRequest.DuracaoEstimada),
                "A duração estimada deve ser maior que zero.")
        };

        _createValidatorMock
            .Setup(validator => validator.ValidateAsync(
                request,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new ValidationResult(validationFailures));

        // Act
        Func<Task> action = async () =>
            await _application.CriarAsync(
                request,
                CancellationToken.None);

        // Assert
        var exception = await action
            .Should()
            .ThrowAsync<ValidationException>();

        exception
            .Which
            .Errors
            .Should()
            .HaveCount(3);

        _rotasRepositoryMock.Verify(
            repository => repository.CriarAsync(
                It.IsAny<Rota>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarRotaResponse_QuandoRotaExistir()
    {
        // Arrange
        var rotaId = Guid.NewGuid();

        var rota = new Rota(
            "São Paulo",
            "Curitiba",
            TimeSpan.FromHours(5));

        DefinirIdDaRota(rota, rotaId);

        _rotasRepositoryMock
            .Setup(repository => repository.ObterPorIdAsync(
                rotaId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(rota);

        // Act
        var resultado = await _application.ObterPorIdAsync(
            rotaId,
            CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();

        resultado!.Id.Should().Be(rotaId);
        resultado.Origem.Should().Be("São Paulo");
        resultado.Destino.Should().Be("Curitiba");

        resultado.DuracaoEstimada.Should()
            .Be(TimeSpan.FromHours(5));

        _rotasRepositoryMock.Verify(
            repository => repository.ObterPorIdAsync(
                rotaId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarNull_QuandoRotaNaoExistir()
    {
        // Arrange
        var rotaId = Guid.NewGuid();

        _rotasRepositoryMock
            .Setup(repository => repository.ObterPorIdAsync(
                rotaId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Rota?)null);

        // Act
        var resultado = await _application.ObterPorIdAsync(
            rotaId,
            CancellationToken.None);

        // Assert
        resultado.Should().BeNull();

        _rotasRepositoryMock.Verify(
            repository => repository.ObterPorIdAsync(
                rotaId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static void DefinirIdDaRota(
        Rota rota,
        Guid id)
    {
        var propriedade = typeof(Rota)
            .GetProperty(nameof(Rota.Id));

        propriedade.Should().NotBeNull();

        propriedade!.SetValue(rota, id);
    }
}