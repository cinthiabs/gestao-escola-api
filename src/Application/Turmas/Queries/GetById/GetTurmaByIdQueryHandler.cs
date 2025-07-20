using Application.Extensions;
using Application.Turmas.Queries.GetAll;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Turmas.Queries.GetById;

public class GetTurmaByIdQueryHandler(ILogger<GetAllTurmasQueryHandler> logger, ITurmaRepository turmaRepository) : IRequestHandler<GetTurmaByIdQuery, Result<GetTurmaByIdViewModel>>
{
    public async Task<Result<GetTurmaByIdViewModel>> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var turma = await turmaRepository.GetByIdTurmaAsync(request.Id, cancellationToken);
            if (turma is null)
                return Result.NoContent();

            return Result.Success(TurmaExtensions.ToGetTurmaByIdViewModel(turma));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_GET_ID_TURMA, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_GET);
        }      
    }
}

