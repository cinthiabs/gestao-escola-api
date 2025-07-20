namespace Application.Alunos.Queries.GetById;

public record struct GetAlunoByIdViewModel(int? Id, string Nome, DateTime DataNascimento, string CPF, string Email);
