namespace Application.Turmas.Queries.GetAll;

public record struct GetAllTurmasViewModel(int? Id, string Nome, string Descricao, int QuantidadeAlunos);
public record struct GetAllTurmasPaginadoViewModel(int PaginaAtual, int TamanhoPagina, int TotalRegistro, int TotalPaginas, IEnumerable<GetAllTurmasViewModel> Turmas);