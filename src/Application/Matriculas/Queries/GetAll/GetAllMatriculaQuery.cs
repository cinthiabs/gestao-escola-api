using Ardalis.Result;
using MediatR;

public record GetAllMatriculaQuery(int NumeroPagina = 1, int TamanhoPagina = 10) : IRequest<Result<GetAllMatriculasPaginadoViewModel>>;