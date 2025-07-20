
using Ardalis.Result;
using MediatR;

namespace Application.Turmas.Commands.Update;

public record UpdateTurmaCommand(int Id, string? Nome, string? Descricao) : IRequest<Result<UpdateTurmaViewModel>>;