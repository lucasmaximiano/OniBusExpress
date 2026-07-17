using OnibusExpress.Application.DTOs.Rotas;

namespace OnibusExpress.Application.DTOs.Viagens
{
    public record ViagemResponse(
        Guid Id,
        Guid RotaId,
        DateTime DataHoraPartida,
        decimal PrecoBase,
        int AssentosDisponiveis,
        RotaResponse Rota);
}
