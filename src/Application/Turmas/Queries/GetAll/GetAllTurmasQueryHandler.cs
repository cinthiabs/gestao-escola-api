using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Turmas.Queries.GetAll;

public class GetAllTurmasQueryHandler(ILogger<GetAllTurmasQueryHandler> logger, ITurmaRepository turmaRepository, IMatriculaRepository matriculaRepository) : IRequestHandler<GetAllTurmasQuery, Result<GetAllTurmasPaginadoViewModel>>
{
    public async Task<Result<GetAllTurmasPaginadoViewModel>> Handle(GetAllTurmasQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var turmas = await turmaRepository.GetAllTurmasPaginadosAsync(request.Nome, request.NumeroPagina, request.TamanhoPagina, cancellationToken);
            if (!turmas.Registros.Any())
                return Result.NoContent();

            foreach (var turma in turmas.Registros)
            {
                var quantidadeAlunos = await matriculaRepository.GetMatriculaByTurmaIdAsync(turma.Id, cancellationToken);
                if (quantidadeAlunos == null)
                {
                    turma.Matriculas = new List<Matricula>();
                    continue;
                }      
                turma.Matriculas = quantidadeAlunos.ToList();
            }

            return Result.Success(TurmaExtensions.ToGetAllTurmasPaginadoViewModel(turmas));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_GET_ALL_TURMAS, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_GET_ALL);
        }
    }
}
