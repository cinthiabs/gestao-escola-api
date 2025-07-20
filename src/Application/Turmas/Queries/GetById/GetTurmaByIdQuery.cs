using Ardalis.Result;
using MediatR;

namespace Application.Turmas.Queries.GetById;

public record GetTurmaByIdQuery(int Id) : IRequest<Result<GetTurmaByIdViewModel>>;