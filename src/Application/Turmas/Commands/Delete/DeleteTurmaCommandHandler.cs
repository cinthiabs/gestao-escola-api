using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Turmas.Commands.Delete;

public class DeleteTurmaCommandHandler(ILogger<DeleteTurmaCommandHandler> logger, ITurmaRepository turmaRepository) : IRequestHandler<DeleteTurmaCommand, Result>
{
    public async Task<Result> Handle(DeleteTurmaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getTurmaById = await turmaRepository.GetByIdTurmaAsync(request.Id, cancellationToken);
            if (getTurmaById == null)
                return Result.Invalid(new ValidationError());

            var deleteTurma = await turmaRepository.DeleteTurmaAsync(request.Id, cancellationToken);
            if (!deleteTurma)
                return Result.Error(NomesErros.ERROR_DELETE);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_DELETE_TURMA, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_DELETE);
        }
    }
}
