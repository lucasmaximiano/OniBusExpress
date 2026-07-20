using FluentValidation;
using FluentValidation.Results;
using Moq;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.Service;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Tests.Application
{
    public class PassageirosApplicationTests
    {
        private readonly Mock<IValidator<CreatePassageiroRequest>>
            _createValidatorMock;

        private readonly Mock<IValidator<UpdatePassageiroRequest>>
            _updateValidatorMock;

        private readonly Mock<IPassageirosRepository>
            _passageirosRepositoryMock;

        private readonly PassageirosApplication _application;

        public PassageirosApplicationTests()
        {
            _createValidatorMock =
                new Mock<IValidator<CreatePassageiroRequest>>();

            _updateValidatorMock =
                new Mock<IValidator<UpdatePassageiroRequest>>();

            _passageirosRepositoryMock =
                new Mock<IPassageirosRepository>();

            _application = new PassageirosApplication(
                _createValidatorMock.Object,
                _updateValidatorMock.Object,
                _passageirosRepositoryMock.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarPassageiro_QuandoRequestForValido()
        {
            // Arrange
            var request = CriarCreateRequestValido();

            ConfigurarCreateValidatorValido();

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

            _passageirosRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.Is<Passageiro>(passageiro =>
                        passageiro.Nome == request.Nome &&
                        passageiro.Cpf == request.Cpf &&
                        passageiro.Email == request.Email &&
                        passageiro.DataNascimento ==
                        request.DataNascimento),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarPassageiro_QuandoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();

            var passageiro = CriarPassageiro();

            _passageirosRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(passageiro);

            // Act
            var response = await _application.ObterPorIdAsync(
                id,
                CancellationToken.None);

            // Assert
            Assert.NotNull(response);

            Assert.Equal(passageiro.Id, response.Id);
            Assert.Equal(passageiro.Nome, response.Nome);
            Assert.Equal(passageiro.Cpf, response.Cpf);
            Assert.Equal(passageiro.Email, response.Email);
            Assert.Equal(
                passageiro.DataNascimento,
                response.DataNascimento);

            _passageirosRepositoryMock.Verify(
                repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_QuandoPassageiroNaoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();

            _passageirosRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Passageiro?)null);

            // Act
            var response = await _application.ObterPorIdAsync(
                id,
                CancellationToken.None);

            // Assert
            Assert.Null(response);

            _passageirosRepositoryMock.Verify(
                repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveAtualizarPassageiro_QuandoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();

            var passageiro = CriarPassageiro();

            var request = new UpdatePassageiroRequest(
                Nome: "Lucas Atualizado",
                Email: "atualizado@email.com",
                DataNascimento: new DateTime(1991, 6, 15));

            ConfigurarUpdateValidatorValido();

            _passageirosRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(passageiro);

            // Act
            var response = await _application.AtualizarAsync(
                id,
                request,
                CancellationToken.None);

            // Assert
            Assert.NotNull(response);

            Assert.Equal(request.Nome, response.Nome);
            Assert.Equal(request.Email, response.Email);
            Assert.Equal(
                request.DataNascimento,
                response.DataNascimento);

            Assert.Equal(passageiro.Cpf, response.Cpf);

            _updateValidatorMock.Verify(
                validator => validator.ValidateAsync(
                    It.IsAny<IValidationContext>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _passageirosRepositoryMock.Verify(
                repository => repository.AtualizarAsync(
                    It.Is<Passageiro>(item =>
                        item.Nome == request.Nome &&
                        item.Email == request.Email &&
                        item.DataNascimento ==
                        request.DataNascimento),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarNull_QuandoPassageiroNaoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = CriarUpdateRequestValido();

            ConfigurarUpdateValidatorValido();

            _passageirosRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Passageiro?)null);

            // Act
            var response = await _application.AtualizarAsync(
                id,
                request,
                CancellationToken.None);

            // Assert
            Assert.Null(response);

            _passageirosRepositoryMock.Verify(
                repository => repository.AtualizarAsync(
                    It.IsAny<Passageiro>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }


        private void ConfigurarCreateValidatorValido()
        {
            _createValidatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }

        private void ConfigurarUpdateValidatorValido()
        {
            _updateValidatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }

        private static CreatePassageiroRequest CriarCreateRequestValido()
        {
            return new CreatePassageiroRequest(
                Nome: "Lucas Maximiano",
                Cpf: "52998224725",
                Email: "lucas@email.com",
                DataNascimento: new DateTime(1990, 5, 10));
        }

        private static UpdatePassageiroRequest CriarUpdateRequestValido()
        {
            return new UpdatePassageiroRequest(
                Nome: "Lucas Maximiano",
                Email: "lucas@email.com",
                DataNascimento: new DateTime(1990, 5, 10));
        }

        private static Passageiro CriarPassageiro()
        {
            return new Passageiro(
                nome: "Lucas Maximiano",
                cpf: "52998224725",
                email: "lucas@email.com",
                dataNascimento: new DateTime(1990, 5, 10));
        }
    }
}