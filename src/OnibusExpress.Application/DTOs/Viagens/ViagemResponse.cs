using OnibusExpress.Application.DTOs.Rotas;

namespace OnibusExpress.Application.DTOs.Viagens
{
    public record ViagemResponse(
        Guid Id,
        Guid RotaId,
        RotaResponse Rota,
        DateTime DataHoraPartida,
        decimal PrecoBase,
        int AssentosDisponiveis);
}
