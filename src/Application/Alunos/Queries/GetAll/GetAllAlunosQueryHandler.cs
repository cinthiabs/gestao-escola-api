using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Alunos.Queries.GetAll;

public class GetAllAlunosQueryHandler(ILogger<GetAllAlunosQueryHandler> logger, IAlunoRepository alunoRepository) : IRequestHandler<GetAllAlunosQuery, Result<GetAllAlunosPaginadoViewModel>>
{
    public async Task<Result<GetAllAlunosPaginadoViewModel>> Handle(GetAllAlunosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var alunos = await alunoRepository.GetAllAlunosPaginadosAsync(request.Nome, request.NumeroPagina, request.TamanhoPagina, cancellationToken);
            if (!alunos.Registros.Any())
                return Result.NoContent();
 
            return Result.Success(AlunoExtensions.ToGetAllAlunosViewModel(alunos));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_GET_ALL_ALUNOS, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_GET_ALL);
        }
    }
}

