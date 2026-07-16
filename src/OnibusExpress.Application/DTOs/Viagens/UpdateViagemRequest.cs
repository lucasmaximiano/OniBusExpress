
namespace OnibusExpress.Application.DTOs.Viagens
{
    public record UpdateViagemRequest(
        Guid RotaId,
        DateTime DataHoraPartida,
        decimal PrecoBase,
        int AssentosDisponiveis);
}
