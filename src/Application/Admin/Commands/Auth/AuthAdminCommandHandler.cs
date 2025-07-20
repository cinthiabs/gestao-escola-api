using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Admin.Commands.Auth;

public class AuthAdminCommandHandler(ILogger<AuthAdminCommandHandler> logger, IAdminRepository adminRepository, IJwtTokenService jwtTokenService) : IRequestHandler<AuthAdminCommand, Result<AuthAdminViewModel>>
{
    public async Task<Result<AuthAdminViewModel>> Handle(AuthAdminCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                return Result<AuthAdminViewModel>.Invalid(new ValidationError(NomesErros.INVALID_CAMPOS_OBRIGATORIOS));

            var GetAdminByEmail = await adminRepository.GetAdminByEmailAsync(request.Email, cancellationToken);
            if (GetAdminByEmail == null)
                return Result<AuthAdminViewModel>.NoContent();

            string tokenAtual = GetAdminByEmail.Token;
            DateTime dataExpiracao = GetAdminByEmail.DataExpiracaoToken ?? DateTime.UtcNow;

            if (TokenPrecisaSerAtualizado(GetAdminByEmail))
            {
                tokenAtual = jwtTokenService.GenerateToken(GetAdminByEmail);
                dataExpiracao = DateTime.Now.AddHours(Variables.TEMPO_EXPIRACAO_TOKEN);

                bool isUpdated = await adminRepository.UpdateAuthAdminAsync(GetAdminByEmail.Id, tokenAtual, dataExpiracao, cancellationToken);
                if (!isUpdated)
                    return Result<AuthAdminViewModel>.Error();
            }

            var authAdminViewModel = new AuthAdminViewModel
            {
                Token = tokenAtual,
                DataExpiracao = dataExpiracao
            };

            return Result<AuthAdminViewModel>.Success(authAdminViewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_AUTH_ADMIN, ex.Message);
            return Result<AuthAdminViewModel>.CriticalError(NomesErros.ERROR);
        }
    }

    private static bool TokenPrecisaSerAtualizado(Administrador admin)
    {
        return string.IsNullOrEmpty(admin.Token)
            || admin.DataExpiracaoToken == null
            || admin.DataExpiracaoToken < DateTime.UtcNow;
    }
}
