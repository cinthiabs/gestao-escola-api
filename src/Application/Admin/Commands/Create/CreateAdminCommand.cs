using Ardalis.Result;
using MediatR;

namespace Application.Admin.Commands.Create;

public record CreateAdminCommand(string Email, string Senha) : IRequest<Result>;
