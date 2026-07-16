
namespace OnibusExpress.Domain.Entities
{
    public class Viagem
    {
        public Guid Id { get; set; }
        public Guid RotaId { get; set; }
        public DateTime DataHoraPartida { get; set; }
        public decimal PrecoBase { get; set; }
        public int AssentosDisponiveis { get; set; }
    }
}
