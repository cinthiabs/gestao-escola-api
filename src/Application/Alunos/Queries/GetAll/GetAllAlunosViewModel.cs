namespace Application.Alunos.Queries.GetAll;

public record struct GetAllAlunosViewModel(int? Id, string Nome, DateTime DataNascimento, string CPF, string Email);

public record struct GetAllAlunosPaginadoViewModel(int PaginaAtual, int TamanhoPagina, int TotalRegistro, int TotalPaginas, IEnumerable<GetAllAlunosViewModel> Alunos);