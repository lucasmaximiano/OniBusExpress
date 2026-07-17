namespace OnibusExpress.Application.DTOs.Passageiros
{
    public record UpdatePassageiroRequest(
    string Nome,
    string Email,
    DateTime DataNascimento);
}
