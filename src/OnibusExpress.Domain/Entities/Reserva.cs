using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Domain.Entities
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public Guid ViagemId { get; set; }
        public Guid PassageiroId { get; set; }
        public int NumeroAssento { get; set; }
        public StatusReserva Status { get; set; }
        public string CodigoReserva { get; set; } = string.Empty;
    }
}
