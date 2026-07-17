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

        public void Cancelar()
        {
            Status = StatusReserva.Cancelada;
        }

        private static string GerarCodigoReserva()
        {
            return $"ONB-{Guid.NewGuid():N}"[..8].ToUpperInvariant();
        }
    }
}
