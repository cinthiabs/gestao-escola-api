using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Matriculas.Queries.GetAll;

public class GetAllMatriculaQueryHandler(ILogger<GetAllMatriculaQueryHandler> logger, IMatriculaRepository matriculaRepository) : IRequestHandler<GetAllMatriculaQuery, Result<GetAllMatriculasPaginadoViewModel>>
{
    public async Task<Result<GetAllMatriculasPaginadoViewModel>> Handle(GetAllMatriculaQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var matriculas = await matriculaRepository.GetAllMatriculaDetalhesAsync(request.NumeroPagina, request.TamanhoPagina, cancellationToken);
            if (!matriculas.Registros.Any())
                return Result.NoContent();

            return Result.Success(MatriculaExtensions.ToGetAllMatriculasPaginadoViewModel(matriculas));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_GET_ALL_MATRICULAS, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_GET_ALL);
        }
    }
}
    