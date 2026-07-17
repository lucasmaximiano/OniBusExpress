namespace OnibusExpress.Domain.Entities
{
    public class Rota
    {
        public Rota(
            string origem,
            string destino,
            TimeSpan duracaoEstimada)
        {
            Id = Guid.NewGuid();
            Origem = origem;
            Destino = destino;
            DuracaoEstimada = duracaoEstimada;
        }

        public Guid Id { get; private set; }
        public string Origem { get; private set; } = string.Empty;
        public string Destino { get; private set; } = string.Empty;
        public TimeSpan DuracaoEstimada { get; private set; }
        public IEnumerable<Viagem> Viagens { get; private set; } = [];

        public void Atualizar(
            string origem,
            string destino,
            TimeSpan duracaoEstimada)
        {
            Origem = origem;
            Destino = destino;
            DuracaoEstimada = duracaoEstimada;
        }
    }
}
