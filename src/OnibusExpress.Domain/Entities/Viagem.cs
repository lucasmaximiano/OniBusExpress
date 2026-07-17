
namespace OnibusExpress.Domain.Entities
{
    public class Viagem
    {
        public Viagem(
           Guid rotaId,
           DateTime dataHoraPartida,
           decimal precoBase,
           int assentosDisponiveis)
        {
            Id = Guid.NewGuid();
            RotaId = rotaId;
            DataHoraPartida = dataHoraPartida;
            PrecoBase = precoBase;
            AssentosDisponiveis = assentosDisponiveis;
        }

        public Guid Id { get; private set; }
        public Guid RotaId { get; private set; }
        public Rota Rota { get; private set; } = null!;
        public DateTime DataHoraPartida { get; private set; }
        public decimal PrecoBase { get; private set; }
        public int AssentosDisponiveis { get; private set; }
        public List<Reserva> Reservas { get; private set; }

        public void Atualizar(
            Guid rotaId,
            DateTime dataHoraPartida,
            decimal precoBase,
            int assentosDisponiveis)
        {
            RotaId = rotaId;
            DataHoraPartida = dataHoraPartida;
            PrecoBase = precoBase;
            AssentosDisponiveis = assentosDisponiveis;
        }
    }
}
