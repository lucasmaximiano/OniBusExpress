using FluentValidation;
using FluentValidation.Results;
using Moq;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.Service;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Enums;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Tests.Application
{

    public class ReservasApplicationTests
    {
        private readonly Mock<IValidator<CreateReservaRequest>>
            _createValidatorMock;

        private readonly Mock<IReservaRepository>
            _reservaRepositoryMock;

        private readonly Mock<IViagensRepository>
            _viagensRepositoryMock;

        private readonly ReservasApplication _application;

        public ReservasApplicationTests()
        {
            _createValidatorMock =
                new Mock<IValidator<CreateReservaRequest>>();

            _reservaRepositoryMock =
                new Mock<IReservaRepository>();

            _viagensRepositoryMock =
                new Mock<IViagensRepository>();

            _application = new ReservasApplication(
                _createValidatorMock.Object,
                _reservaRepositoryMock.Object,
                _viagensRepositoryMock.Object);
        }

        [Fact]
        public async Task CriarAsync_DeveCriarReserva_QuandoDadosForemValidos()
        {
            // Arrange
            var request = CriarRequestValido();

            var viagem = CriarViagem(
                dataHoraPartida: DateTime.Now.AddDays(1),
                assentosDisponiveis: 40);

            ConfigurarValidatorValido();

            _viagensRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(viagem);

            _reservaRepositoryMock
                .Setup(repository =>
                    repository.ObterReservaPorViagemEAssentoAsync(
                        request.ViagemId,
                        request.NumeroAssento,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync((Reserva?)null);

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
                repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _reservaRepositoryMock.Verify(
                repository =>
                    repository.ObterReservaPorViagemEAssentoAsync(
                        request.ViagemId,
                        request.NumeroAssento,
                        It.IsAny<CancellationToken>()),
                Times.Once);

            _reservaRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.Is<Reserva>(reserva =>
                        reserva.ViagemId == request.ViagemId &&
                        reserva.PassageiroId == request.PassageiroId &&
                        reserva.NumeroAssento == request.NumeroAssento),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }


        [Fact]
        public async Task CriarAsync_DeveLancarKeyNotFoundException_QuandoViagemNaoExistir()
        {
            // Arrange
            var request = CriarRequestValido();

            ConfigurarValidatorValido();

            _viagensRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Viagem?)null);

            // Act
            var exception =
                await Assert.ThrowsAsync<KeyNotFoundException>(
                    () => _application.CriarAsync(
                        request,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "Viagem não encontrada.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task CriarAsync_DeveLancarInvalidOperationException_QuandoViagemJaTiverOcorrido()
        {
            // Arrange
            var request = CriarRequestValido();

            var viagem = CriarViagem(
                dataHoraPartida: DateTime.Now.AddHours(-1),
                assentosDisponiveis: 40);

            ConfigurarValidatorValido();

            _viagensRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(viagem);

            // Act
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _application.CriarAsync(
                        request,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "Não é possível reservar passagem para uma viagem já realizada.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository =>
                    repository.ObterReservaPorViagemEAssentoAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()),
                Times.Never);

            _reservaRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task CriarAsync_DeveLancarInvalidOperationException_QuandoNumeroAssentoExcederCapacidade()
        {
            // Arrange
            var request = new CreateReservaRequest(
                Guid.NewGuid(),
                Guid.NewGuid(),
                41);

            var viagem = CriarViagem(
                dataHoraPartida: DateTime.Now.AddDays(1),
                assentosDisponiveis: 40);

            ConfigurarValidatorValido();

            _viagensRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(viagem);

            // Act
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _application.CriarAsync(
                        request,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "O número do assento informado excede a quantidade de assentos disponíveis para esta viagem.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository =>
                    repository.ObterReservaPorViagemEAssentoAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()),
                Times.Never);

            _reservaRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task CriarAsync_DeveLancarInvalidOperationException_QuandoAssentoEstiverOcupado()
        {
            // Arrange
            var request = CriarRequestValido();

            var viagem = CriarViagem(
                dataHoraPartida: DateTime.Now.AddDays(1),
                assentosDisponiveis: 40);

            var reservaExistente = new Reserva(
                request.ViagemId,
                Guid.NewGuid(),
                request.NumeroAssento);

            ConfigurarValidatorValido();

            _viagensRepositoryMock
                .Setup(repository => repository.ObterPorIdAsync(
                    request.ViagemId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(viagem);

            _reservaRepositoryMock
                .Setup(repository =>
                    repository.ObterReservaPorViagemEAssentoAsync(
                        request.ViagemId,
                        request.NumeroAssento,
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(reservaExistente);

            // Act
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _application.CriarAsync(
                        request,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                $"O assento {request.NumeroAssento} já está ocupado nesta viagem.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository => repository.CriarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ObterPorCodigoAsync_DeveRetornarNull_QuandoReservaNaoExistir()
        {
            // Arrange
            const string codigoReserva = "ABC123";

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    codigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Reserva?)null);

            // Act
            var response = await _application.ObterPorCodigoAsync(
                codigoReserva,
                CancellationToken.None);

            // Assert
            Assert.Null(response);

            _reservaRepositoryMock.Verify(
                repository => repository.ObterPorCodigoAsync(
                    codigoReserva,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ObterPorCodigoAsync_DeveRetornarReserva_QuandoExistir()
        {
            // Arrange
            var reserva = CriarReservaCompleta();
            var codigoReserva = reserva.CodigoReserva;

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    codigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(reserva);

            // Act
            var response = await _application.ObterPorCodigoAsync(
                codigoReserva,
                CancellationToken.None);

            // Assert
            Assert.NotNull(response);

            Assert.Equal(reserva.Id, response.Id);
            Assert.Equal(reserva.ViagemId, response.ViagemId);
            Assert.Equal(reserva.PassageiroId, response.PassageiroId);
            Assert.Equal(reserva.NumeroAssento, response.NumeroAssento);
            Assert.Equal(reserva.Status, response.Status);
            Assert.Equal(reserva.CodigoReserva, response.CodigoReserva);

            Assert.Equal(
                reserva.Viagem.DataHoraPartida,
                response.Viagem.DataHoraPartida);

            Assert.Equal(
                reserva.Viagem.Rota.Origem,
                response.Viagem.Rota.Origem);

            Assert.Equal(
                reserva.Passageiro.Nome,
                response.Passageiro.Nome);
        }

        [Fact]
        public async Task CancelarAsync_DeveCancelarReserva_QuandoCancelamentoForPermitido()
        {
            // Arrange
            var reserva = CriarReservaCompleta(
                dataHoraPartida: DateTime.Now.AddHours(5));

            var codigoReserva = reserva.CodigoReserva;

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    codigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(reserva);

            // Act
            await _application.CancelarAsync(
                codigoReserva,
                CancellationToken.None);

            // Assert
            Assert.Equal(
                StatusReserva.Cancelada,
                reserva.Status);

            _reservaRepositoryMock.Verify(
                repository => repository.CancelarAsync(
                    reserva,
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CancelarAsync_DeveLancarKeyNotFoundException_QuandoReservaNaoExistir()
        {
            // Arrange
            const string codigoReserva = "INEXISTENTE";

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    codigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Reserva?)null);

            // Act
            var exception =
                await Assert.ThrowsAsync<KeyNotFoundException>(
                    () => _application.CancelarAsync(
                        codigoReserva,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "Reserva não encontrada.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository => repository.CancelarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task CancelarAsync_DeveLancarInvalidOperationException_QuandoReservaJaEstiverCancelada()
        {
            // Arrange
            var reserva = CriarReservaCompleta(
                dataHoraPartida: DateTime.Now.AddHours(5));

            reserva.Cancelar();

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    reserva.CodigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(reserva);

            // Act
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _application.CancelarAsync(
                        reserva.CodigoReserva,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "A reserva já está cancelada.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository => repository.CancelarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task CancelarAsync_DeveLancarInvalidOperationException_QuandoEstiverForaDoPrazo(
            int horasAtePartida)
        {
            // Arrange
            var reserva = CriarReservaCompleta(
                dataHoraPartida: DateTime.Now.AddHours(horasAtePartida));

            _reservaRepositoryMock
                .Setup(repository => repository.ObterPorCodigoAsync(
                    reserva.CodigoReserva,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(reserva);

            // Act
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _application.CancelarAsync(
                        reserva.CodigoReserva,
                        CancellationToken.None));

            // Assert
            Assert.Equal(
                "O cancelamento só é permitido até 2 horas antes da partida.",
                exception.Message);

            _reservaRepositoryMock.Verify(
                repository => repository.CancelarAsync(
                    It.IsAny<Reserva>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        private void ConfigurarValidatorValido()
        {
            _createValidatorMock
                .Setup(validator => validator.ValidateAsync(
                    It.IsAny<IValidationContext>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
        }
     
        private static CreateReservaRequest CriarRequestValido()
        {
            return new CreateReservaRequest(
                ViagemId: Guid.NewGuid(),
                PassageiroId: Guid.NewGuid(),
                NumeroAssento: 10);
        }

        private static Viagem CriarViagem(
            DateTime dataHoraPartida,
            int assentosDisponiveis)
        {
            return new Viagem(
                rotaId: Guid.NewGuid(),
                dataHoraPartida: dataHoraPartida,
                precoBase: 150m,
                assentosDisponiveis: assentosDisponiveis);
        }

        private static Reserva CriarReservaCompleta(
            DateTime? dataHoraPartida = null)
        {
            var rota = new Rota(
                origem: "São Paulo",
                destino: "Campinas",
                duracaoEstimada: TimeSpan.FromHours(2));

            var viagem = new Viagem(
                rotaId: rota.Id,
                dataHoraPartida:
                    dataHoraPartida ?? DateTime.Now.AddHours(5),
                precoBase: 150m,
                assentosDisponiveis: 40);

            var passageiro = new Passageiro(
                nome: "Lucas Maximiano",
                cpf: "52998224725",
                email: "lucas@email.com",
                dataNascimento: new DateTime(1990, 5, 10));

            var reserva = new Reserva(
                viagemId: viagem.Id,
                passageiroId: passageiro.Id,
                numeroAssento: 10);

            DefinirPropriedade(reserva, nameof(Reserva.Viagem), viagem);
            DefinirPropriedade(reserva, nameof(Reserva.Passageiro), passageiro);
            DefinirPropriedade(viagem, nameof(Viagem.Rota), rota);

            return reserva;
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

            propriedade.SetValue(objeto, valor);
        }
    }
}
