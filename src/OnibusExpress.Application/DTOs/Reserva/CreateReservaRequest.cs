
namespace OnibusExpress.Application.DTOs.Reserva
{
    public record CreateReservaRequest(
        Guid ViagemId,
        Guid PassageiroId,
        int NumeroAssento);
}
