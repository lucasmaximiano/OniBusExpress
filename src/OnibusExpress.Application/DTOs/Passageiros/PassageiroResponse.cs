namespace OnibusExpress.Application.DTOs.Passageiros
{
    public record PassageiroResponse(
     Guid Id,
     string Nome,
     string Cpf,
     string Email,
     DateOnly DataNascimento);
}
