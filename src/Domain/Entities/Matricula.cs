namespace Domain.Entities;

public class Matricula
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }

    public Matricula(int alunoId, int turmaId)
    {
        AlunoId = alunoId;
        TurmaId = turmaId;
        DataMatricula = DateTime.UtcNow;
    }
    public Matricula() { }
}
public class MatriculaDetalhe
{
    public int Id { get; set; }
    public string Aluno { get; set; } = default!;
    public string Turma { get; set; } = default!;
    public DateTime DataMatricula { get; set; }
}
