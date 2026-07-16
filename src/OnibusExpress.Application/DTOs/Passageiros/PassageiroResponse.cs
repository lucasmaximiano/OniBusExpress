namespace OnibusExpress.Application.DTOs.Passageiros
{
    public sealed record PassageiroResponse(
     Guid Id,
     string Nome,
     string Cpf,
     string Email,
     DateOnly DataNascimento);
}
