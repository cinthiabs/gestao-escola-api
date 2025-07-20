using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Turmas.Commands.Create;

public class CreateTurmaCommandHandler(ILogger<CreateTurmaCommandHandler> logger, IValidator<CreateTurmaCommand> validator, ITurmaRepository turmaRepository) : IRequestHandler<CreateTurmaCommand, Result>
{
    public async Task<Result> Handle(CreateTurmaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorCommand = validator.Validate(request);
            var result = ValidationResultExtensions.ToResult(validatorCommand);
            if (!result.IsSuccess)
                return Result.Invalid(result.ValidationErrors);

            var createTurma = await turmaRepository.CreateTurmaAsync(TurmaExtensions.ToTurma(request),cancellationToken);

            if (!createTurma)
                return Result.Error(NomesErros.ERROR_CREATE);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_CREATE_TURMA, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_CREATE);
        }
    }
}
