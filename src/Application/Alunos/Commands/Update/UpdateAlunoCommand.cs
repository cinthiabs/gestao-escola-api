using Ardalis.Result;
using MediatR;

namespace Application.Alunos.Commands.Update;

public record UpdateAlunoCommand(int Id, string? Nome, DateTime DataNascimento, string? CPF, string? Email, string? Senha) : IRequest<Result<UpdateAlunoViewModel>>;