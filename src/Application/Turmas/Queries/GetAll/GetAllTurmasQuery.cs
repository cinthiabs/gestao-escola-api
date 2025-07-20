using Ardalis.Result;
using MediatR;

namespace Application.Turmas.Queries.GetAll;

public record GetAllTurmasQuery(string? Nome, int NumeroPagina = 1, int TamanhoPagina = 10) : IRequest<Result<GetAllTurmasPaginadoViewModel>>;
