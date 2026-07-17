using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Entities
{
    public class Reserva
    {
        public Reserva(
           Guid viagemId,
           Guid passageiroId,
           int numeroAssento)
        {
            Id = Guid.NewGuid();
            ViagemId = viagemId;
            PassageiroId = passageiroId;
            NumeroAssento = numeroAssento;
            Status = StatusReserva.Pendente;
            CodigoReserva = GerarCodigoReserva();
        }

        public Guid Id { get; private set; }
        public Guid ViagemId { get; private set; }
        public Viagem Viagem { get; private set; } = null!;
        public Guid PassageiroId { get; private set; }
        public Passageiro Passageiro { get; private set; } = null!;
        public int NumeroAssento { get; private set; }
        public StatusReserva Status { get; private set; }
        public string CodigoReserva { get; private set; } = string.Empty;

        public void AlterarAssento(int numeroAssento)
        {
            if (Status == StatusReserva.Cancelada)
                throw new InvalidOperationException(
                    "Não é possível alterar uma reserva cancelada.");

            NumeroAssento = numeroAssento;
        }

        public void Confirmar()
        {
            if (Status == StatusReserva.Cancelada)
                throw new InvalidOperationException(
                    "Não é possível confirmar uma reserva cancelada.");

            Status = StatusReserva.Confirmada;
        }

        public void Cancelar()
        {
            if (Status == StatusReserva.Concluida)
                throw new InvalidOperationException(
                    "Não é possível cancelar uma reserva concluída.");

            Status = StatusReserva.Cancelada;
        }

        public void Concluir()
        {
            if (Status != StatusReserva.Confirmada)
                throw new InvalidOperationException(
                    "Somente reservas confirmadas podem ser concluídas.");

            Status = StatusReserva.Concluida;
        }

        private static string GerarCodigoReserva()
        {
            return $"ONB-{Guid.NewGuid():N}"[..12].ToUpperInvariant();
        }
    }
}
