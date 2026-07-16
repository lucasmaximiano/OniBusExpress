namespace OnibusExpress.Application.DTOs.Viagens
{
    public record CreateViagemRequest(
        Guid RotaId,
        DateTime DataHoraPartida,
        decimal PrecoBase,
        int AssentosDisponiveis);
}
