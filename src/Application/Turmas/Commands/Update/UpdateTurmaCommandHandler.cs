using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Turmas.Commands.Update;

public class UpdateTurmaCommandHandler(ILogger<UpdateTurmaCommandHandler> logger, IValidator<UpdateTurmaCommand> validator, ITurmaRepository turmaRepository) : IRequestHandler<UpdateTurmaCommand, Result<UpdateTurmaViewModel>>
{
    public async Task<Result<UpdateTurmaViewModel>> Handle(UpdateTurmaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorCommand = validator.Validate(request);

            var result = ValidationResultExtensions.ToResult(validatorCommand);
            if (!result.IsSuccess)
                return Result.Invalid(result.ValidationErrors);

            var getTurmaById = await turmaRepository.GetByIdTurmaAsync(request.Id, cancellationToken);
            if (getTurmaById == null)
                return Result.Invalid();

            var updateTurma = await turmaRepository.UpdateTurmaAsync(new Turma(request.Nome, request.Descricao, request.Id), cancellationToken);

            if (!updateTurma)
                return Result.Error(NomesErros.ERROR_UPDATE);

            var getTurmaAtualizada = await turmaRepository.GetByIdTurmaAsync(request.Id, cancellationToken);

            return TurmaExtensions.ToUpdateTurmaViewModel(getTurmaAtualizada);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_UPDATE_TURMA, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_UPDATE);
        }
    }
}
