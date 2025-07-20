using Ardalis.Result;
using MediatR;

namespace Application.Turmas.Commands.Delete;

public record DeleteTurmaCommand(int Id) : IRequest<Result>;
