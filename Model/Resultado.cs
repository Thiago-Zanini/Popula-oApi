namespace Populacao.Model
{
    public class Resultado<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Dados { get; set; }
    }
}
