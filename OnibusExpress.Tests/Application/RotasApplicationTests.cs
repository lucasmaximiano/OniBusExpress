using FluentValidation;
using FluentValidation.Results;
using Moq;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.Service;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Tests.Application
{
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
            var request = new CreateRotaRequest(
                "São Paulo", 
                "Rio de Janeiro", 
                TimeSpan.FromHours(6));

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
            await _application.CriarAsync(request, default);

            // Assert
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
        public async Task ObterPorIdAsync_DeveRetornarRotaResponse_QuandoRotaExistir()
        {
            // Arrange
            var rotaId = Guid.NewGuid();

            var rota = new Rota(
                "São Paulo",
                "Curitiba",
                TimeSpan.FromHours(5));

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
            _rotasRepositoryMock.Verify(
                repository => repository.ObterPorIdAsync(
                    rotaId,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}