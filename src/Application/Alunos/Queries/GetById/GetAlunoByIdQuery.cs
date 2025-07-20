using Ardalis.Result;
using MediatR;

namespace Application.Alunos.Queries.GetById;

public record GetAlunoByIdQuery(int Id) : IRequest<Result<GetAlunoByIdViewModel>>;