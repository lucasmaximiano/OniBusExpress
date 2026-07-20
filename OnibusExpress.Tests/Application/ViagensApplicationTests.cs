using FluentValidation;
using FluentValidation.Results;
using Moq;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Service;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Tests.Application;

public class ViagensApplicationTests
{
    private readonly Mock<IValidator<CreateViagemRequest>>
        _createValidatorMock;

    private readonly Mock<IViagensRepository>
        _viagensRepositoryMock;

    private readonly ViagensApplication _application;

    public ViagensApplicationTests()
    {
        _createValidatorMock =
            new Mock<IValidator<CreateViagemRequest>>();

        _viagensRepositoryMock =
            new Mock<IViagensRepository>();

        _application = new ViagensApplication(
            _createValidatorMock.Object,
            _viagensRepositoryMock.Object);
    }

    [Fact]
    public async Task CriarAsync_DeveCriarViagem_QuandoRequestForValido()
    {
        // Arrange
        var request = CriarRequestValido();

        ConfigurarValidatorValido();

        // Act
        await _application.CriarAsync(
            request,
            CancellationToken.None);

        // Assert
        _createValidatorMock.Verify(
            validator => validator.ValidateAsync(
                It.IsAny<IValidationContext>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _viagensRepositoryMock.Verify(
            repository => repository.CriarAsync(
                It.Is<Viagem>(viagem =>
                    viagem.RotaId == request.RotaId &&
                    viagem.DataHoraPartida == request.DataHoraPartida &&
                    viagem.PrecoBase == request.PrecoBase &&
                    viagem.AssentosDisponiveis ==
                    request.AssentosDisponiveis),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarNull_QuandoViagemNaoExistir()
    {
        // Arrange
        var id = Guid.NewGuid();

        _viagensRepositoryMock
            .Setup(repository => repository.ObterPorIdAsync(
                id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Viagem?)null);

        // Act
        var response = await _application.ObterPorIdAsync(
            id,
            CancellationToken.None);

        // Assert
        Assert.Null(response);

        _viagensRepositoryMock.Verify(
            repository => repository.ObterPorIdAsync(
                id,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarViagem_QuandoExistir()
    {
        // Arrange
        var rota = CriarRota();

        var viagem = CriarViagemComRota(
            rota,
            DateTime.Now.AddDays(1),
            180m,
            42);

        _viagensRepositoryMock
            .Setup(repository => repository.ObterPorIdAsync(
                viagem.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(viagem);

        // Act
        var response = await _application.ObterPorIdAsync(
            viagem.Id,
            CancellationToken.None);

        // Assert
        Assert.NotNull(response);

        Assert.Equal(viagem.Id, response.Id);
        Assert.Equal(viagem.RotaId, response.RotaId);
        Assert.Equal(
            viagem.DataHoraPartida,
            response.DataHoraPartida);

        Assert.Equal(
            viagem.PrecoBase,
            response.PrecoBase);

        Assert.Equal(
            viagem.AssentosDisponiveis,
            response.AssentosDisponiveis);

        Assert.NotNull(response.Rota);

        Assert.Equal(
            rota.Id,
            response.Rota.Id);

        Assert.Equal(
            rota.Origem,
            response.Rota.Origem);

        Assert.Equal(
            rota.Destino,
            response.Rota.Destino);

        Assert.Equal(
            rota.DuracaoEstimada,
            response.Rota.DuracaoEstimada);

        _viagensRepositoryMock.Verify(
            repository => repository.ObterPorIdAsync(
                viagem.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ObterPorFiltroAsync_DeveRetornarViagensMapeadas()
    {
        // Arrange
        const string origem = "São Paulo";
        const string destino = "Campinas";

        var rota = CriarRota(
            origem,
            destino);

        var viagens = new List<Viagem>
        {
            CriarViagemComRota(
                rota,
                DateTime.Now.AddDays(1),
                150m,
                40),

            CriarViagemComRota(
                rota,
                DateTime.Now.AddDays(2),
                175m,
                42)
        };

        _viagensRepositoryMock
            .Setup(repository => repository.ObterPorFiltroAsync(
                origem,
                destino,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(viagens);

        // Act
        var response = await _application.ObterPorFiltroAsync(
            origem,
            destino,
            CancellationToken.None);

        var resultado = response.ToList();

        // Assert
        Assert.Equal(
            2,
            resultado.Count);

        Assert.All(
            resultado,
            item =>
            {
                Assert.Equal(
                    origem,
                    item.Rota.Origem);

                Assert.Equal(
                    destino,
                    item.Rota.Destino);
            });

        Assert.Equal(
            viagens[0].Id,
            resultado[0].Id);

        Assert.Equal(
            viagens[0].PrecoBase,
            resultado[0].PrecoBase);

        Assert.Equal(
            viagens[1].Id,
            resultado[1].Id);

        Assert.Equal(
            viagens[1].PrecoBase,
            resultado[1].PrecoBase);

        _viagensRepositoryMock.Verify(
            repository => repository.ObterPorFiltroAsync(
                origem,
                destino,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ObterPorFiltroAsync_DeveRetornarListaVazia_QuandoNaoExistiremViagens()
    {
        // Arrange
        const string origem = "São Paulo";
        const string destino = "Rio de Janeiro";

        _viagensRepositoryMock
            .Setup(repository => repository.ObterPorFiltroAsync(
                origem,
                destino,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var response = await _application.ObterPorFiltroAsync(
            origem,
            destino,
            CancellationToken.None);

        // Assert
        Assert.Empty(response);

        _viagensRepositoryMock.Verify(
            repository => repository.ObterPorFiltroAsync(
                origem,
                destino,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private void ConfigurarValidatorValido()
    {
        _createValidatorMock
            .Setup(validator => validator.ValidateAsync(
                It.IsAny<IValidationContext>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
    }

    private static CreateViagemRequest CriarRequestValido()
    {
        return new CreateViagemRequest(
            RotaId: Guid.NewGuid(),
            DataHoraPartida: DateTime.Now.AddDays(1),
            PrecoBase: 150m,
            AssentosDisponiveis: 40);
    }

    private static Rota CriarRota(
        string origem = "São Paulo",
        string destino = "Campinas")
    {
        return new Rota(
            origem,
            destino,
            TimeSpan.FromHours(2));
    }

    private static Viagem CriarViagemComRota(
        Rota rota,
        DateTime dataHoraPartida,
        decimal precoBase,
        int assentosDisponiveis)
    {
        var viagem = new Viagem(
            rota.Id,
            dataHoraPartida,
            precoBase,
            assentosDisponiveis);

        DefinirPropriedade(
            viagem,
            nameof(Viagem.Rota),
            rota);

        return viagem;
    }

    private static void DefinirPropriedade<T>(
        object objeto,
        string nomePropriedade,
        T valor)
    {
        var propriedade = objeto
            .GetType()
            .GetProperty(nomePropriedade);

        if (propriedade is null)
        {
            throw new InvalidOperationException(
                $"A propriedade {nomePropriedade} não foi encontrada.");
        }

        propriedade.SetValue(
            objeto,
            valor);
    }
}