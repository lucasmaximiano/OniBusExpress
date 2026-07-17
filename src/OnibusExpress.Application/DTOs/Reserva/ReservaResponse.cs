using OnibusExpress.Application.DTOs.Passageiros;
using OnibusExpress.Application.DTOs.Viagens;
using OnibusExpress.Domain.Enums;

namespace OnibusExpress.Application.DTOs.Reserva
{
    public record ReservaResponse(
      Guid Id,
      Guid ViagemId,
      ViagemResponse Viagem,
      Guid PassageiroId,
      PassageiroResponse Passageiro,
      int NumeroAssento,
      StatusReserva Status,
      string CodigoReserva);
}
