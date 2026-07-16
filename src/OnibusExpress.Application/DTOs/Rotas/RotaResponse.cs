namespace OnibusExpress.Application.DTOs.Rotas
{
    public record RotaResponse(
      Guid Id,
      string Origem,
      string Destino,
      TimeSpan DuracaoEstimada);
}
