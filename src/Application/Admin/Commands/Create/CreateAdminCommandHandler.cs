using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Admin.Commands.Create;

public class CreateAdminCommandHandler(ILogger<CreateAdminCommandHandler> logger, IAdminRepository adminRepository, IValidator<CreateAdminCommand> validator) : IRequestHandler<CreateAdminCommand, Result>
{
    public async Task<Result> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorCommand = validator.Validate(request);
            var result = ValidationResultExtensions.ToResult(validatorCommand);
            if (!result.IsSuccess)
                return Result.Invalid(result.ValidationErrors);

            var verificaSeExisteAdminCadastrado = await adminRepository.ExisteAdminAsync(request.Email, cancellationToken);
            if (verificaSeExisteAdminCadastrado)
                return Result.Conflict(NomesErros.CONFLITO);

            var criarSenhaAdmin = Administrador.Criar(request.Email, request.Senha);

            var criaNovoAdmin = await adminRepository.CreateAdminAsync(criarSenhaAdmin, cancellationToken);
            if (!criaNovoAdmin)
                return Result.Error(NomesErros.ERROR_CREATE);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_CREATE_ALUNO, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_CREATE);
        }
    }

}
