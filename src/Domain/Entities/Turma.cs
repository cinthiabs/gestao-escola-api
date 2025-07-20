namespace Domain.Entities;

public class Turma
{
    public int? Id { get; set; }
    public string Nome { get; set; } = default!;
    public string Descricao { get; set; } = default!;

    public Turma() { }

    public Turma(string nome, string descricao, int? id = null)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
    }
    public ICollection<Matricula>? Matriculas { get; set; } = new List<Matricula>();
}
