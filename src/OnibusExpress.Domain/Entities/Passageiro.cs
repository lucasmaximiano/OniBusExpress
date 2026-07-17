
namespace OnibusExpress.Domain.Entities
{
    public class Passageiro
    {
        public Passageiro(
            string nome,
            string cpf,
            string email,
            DateTime dataNascimento)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cpf = cpf;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DataNascimento { get; private set; }
        public IEnumerable<Reserva> Reservas { get; private set; } = [];

        public void Atualizar(
            string nome,
            string email,
            DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }
    }
}
