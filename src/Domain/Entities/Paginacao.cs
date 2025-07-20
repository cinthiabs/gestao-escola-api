namespace Domain.Entities;

public class Paginacao<T>
{
    public int PaginaAtual { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalRegistro { get; set; }
    public IEnumerable<T> Registros { get; set; } = Enumerable.Empty<T>();
}
