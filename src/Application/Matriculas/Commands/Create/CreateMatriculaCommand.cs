
using Ardalis.Result;
using MediatR;

public record CreateMatriculaCommand(int AlunoId, int TurmaId) : IRequest<Result>;

