using FluentValidation;
using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.DTOs.Reserva;
using OnibusExpress.Application.DTOs.Rotas;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Application.Interfaces;
using OnibusExpress.Domain.Entities;
using OnibusExpress.Domain.Enums;
using OnibusExpress.Domain.Interfaces;

namespace OnibusExpress.Application.Service
{
    public class ReservasApplication(
        IValidator<CreateReservaRequest> createValidator, 
        IReservaRepository reservaRepository,
        IViagensRepository viagensRepository) : IReservasApplication
    {
        private readonly IValidator<CreateReservaRequest> _createValidator = createValidator;
        private readonly IReservaRepository _reservaRepository = reservaRepository;
        private readonly IViagensRepository _viagensRepository = viagensRepository;

        public async Task CriarAsync(
            CreateReservaRequest request, 
            CancellationToken cancellationToken)
        {
            await _createValidator.ValidateAndThrowAsync(
                 request,
                 cancellationToken);

            Viagem? viagem = await _viagensRepository.ObterPorIdAsync(
               request.ViagemId,
               cancellationToken);

            if (viagem is null)
                throw new KeyNotFoundException("Viagem não encontrada.");

            if (viagem.DataHoraPartida <= DateTime.Now)
                throw new InvalidOperationException(
                    "Não é possível reservar passagem para uma viagem já realizada.");

            if (viagem.AssentosDisponiveis < request.NumeroAssento)
                throw new InvalidOperationException(
                    "O número do assento informado excede a quantidade de assentos disponíveis para esta viagem.");

            Reserva? assentoOcupado = await _reservaRepository.ObterReservaPorViagemEAssentoAsync(
                request.ViagemId,
                request.NumeroAssento,
                cancellationToken);

            if (assentoOcupado != null)
                throw new InvalidOperationException(
                    $"O assento {request.NumeroAssento} já está ocupado nesta viagem.");

            Reserva reserva = new(
                        request.ViagemId, 
                        request.PassageiroId, 
                        request.NumeroAssento);

            await _reservaRepository.CriarAsync(
                        reserva,
                        cancellationToken);
        }

        public async Task<ReservaResponse?> ObterPorCodigoAsync(
            string codigoReserva,
            CancellationToken cancellationToken)
        {
            Reserva? reserva = await _reservaRepository.ObterPorCodigoAsync(
                                                               codigoReserva,
                                                               cancellationToken);
            if (reserva is null)
                return null;

            return reserva is null
                ? null
                : MapToResponse(reserva);
        }

        public async Task CancelarAsync(
            string codigoReserva,
            CancellationToken cancellationToken)
        {
            Reserva? reserva = await _reservaRepository.ObterPorCodigoAsync(
                                                    codigoReserva,
                                                    cancellationToken);

            if (reserva is null)
                throw new KeyNotFoundException("Reserva não encontrada.");

            if (reserva.Status == StatusReserva.Cancelada)
                throw new InvalidOperationException("A reserva já está cancelada.");

            if (DateTime.Now >= reserva.Viagem.DataHoraPartida.AddHours(-2))
                throw new InvalidOperationException(
                    "O cancelamento só é permitido até 2 horas antes da partida.");

            reserva.Cancelar();

            await _reservaRepository.CancelarAsync(
                reserva,
                cancellationToken);
        }

        private static ReservaResponse MapToResponse(Reserva reserva)
        {
            return new ReservaResponse(
                reserva.Id,
                reserva.ViagemId,
                new ViagemResponse(
                    reserva.Viagem.Id,
                    reserva.Viagem.RotaId,
                    reserva.Viagem.DataHoraPartida,
                    reserva.Viagem.PrecoBase,
                    reserva.Viagem.AssentosDisponiveis,
                    new RotaResponse(
                        reserva.Viagem.Rota.Id,
                        reserva.Viagem.Rota.Origem,
                        reserva.Viagem.Rota.Destino,
                        reserva.Viagem.Rota.DuracaoEstimada)
                ),
                reserva.PassageiroId,
                new PassageiroResponse(
                    reserva.Passageiro.Id,
                    reserva.Passageiro.Nome,
                    reserva.Passageiro.Cpf,
                    reserva.Passageiro.Email,
                    reserva.Passageiro.DataNascimento
                ),
                reserva.NumeroAssento,
                reserva.Status,
                reserva.CodigoReserva
            );
        }
    }
}
