using Ardalis.Result;
using MediatR;

namespace Application.Turmas.Commands.Create;

public record CreateTurmaCommand(string Nome, string Descricao) : IRequest<Result>;