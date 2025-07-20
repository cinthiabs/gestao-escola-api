public record struct GetAllMatriculasViewModel(int Id, string Aluno, string Turma, DateTime DataMatricula);

public record struct GetAllMatriculasPaginadoViewModel(int PaginaAtual, int TamanhoPagina, int TotalRegistro, int TotalPaginas, IEnumerable<GetAllMatriculasViewModel> Matriculas);