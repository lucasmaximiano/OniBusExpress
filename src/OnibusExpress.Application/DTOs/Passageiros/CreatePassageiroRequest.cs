namespace OnibusExpress.Application.DTOs.Passageiros
{
    public record CreatePassageiroRequest(
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento);
}
