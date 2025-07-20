using Ardalis.Result;
using MediatR;

namespace Application.Alunos.Commands.Delete;

public record DeleteAlunoCommand(int Id) : IRequest<Result>;
