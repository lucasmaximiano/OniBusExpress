namespace OnibusExpress.Domain.Entities
{
    public class Rota
    {
        public Guid Id { get; set; }
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public TimeSpan DuracaoEstimada { get; set; }
    }
}
