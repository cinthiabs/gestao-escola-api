using MediatR;
using Ardalis.Result;
namespace Application.Alunos.Commands.Create;

public record CreateAlunoCommand(string Nome, DateTime DataNascimento, string CPF, string Email, string Senha) : IRequest<Result>;