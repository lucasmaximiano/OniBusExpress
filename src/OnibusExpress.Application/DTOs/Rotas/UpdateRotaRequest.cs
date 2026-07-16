namespace OnibusExpress.Application.DTOs.Rotas
{
    public record UpdateRotaRequest(
     string Origem,
     string Destino,
     TimeSpan DuracaoEstimada);
}
