using Ardalis.Result;
using MediatR;

namespace Application.Admin.Commands.Auth;

public record AuthAdminCommand(string Email, string Senha) : IRequest<Result<AuthAdminViewModel>>;
