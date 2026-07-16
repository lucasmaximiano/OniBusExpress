
namespace OnibusExpress.Application.DTOs.Rotas
{
    public record CreateRotaRequest(
    string Origem,
    string Destino,
    TimeSpan DuracaoEstimada);
}
