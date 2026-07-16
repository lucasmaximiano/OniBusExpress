
namespace OnibusExpress.Domain.Entities
{
    public class Passageiro
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
    }
}
