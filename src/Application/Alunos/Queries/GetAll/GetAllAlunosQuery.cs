using System.Runtime.CompilerServices;
using Ardalis.Result;
using MediatR;

namespace Application.Alunos.Queries.GetAll;

public record GetAllAlunosQuery(string? Nome, int NumeroPagina = 1, int TamanhoPagina = 10) : IRequest<Result<GetAllAlunosPaginadoViewModel>>;