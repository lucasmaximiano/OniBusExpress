using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Application.DTOs.Reserva
{
    public sealed record UpdateReservaRequest(
       int NumeroAssento,
       StatusReserva Status);
}
