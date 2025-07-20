using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Matriculas.Commands.Create;

public class CreateMatriculaCommandHandler(ILogger<CreateMatriculaCommandHandler> logger, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository,  IMatriculaRepository matriculaRepository) : IRequestHandler<CreateMatriculaCommand, Result>
{
    public async Task<Result> Handle(CreateMatriculaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var alunoExiste = await alunoRepository.GetByIdAlunoAsync(request.AlunoId, cancellationToken);
            if (alunoExiste == null)
                return Result.Invalid(new ValidationError("AlunoId", NomesErros.ERRO_ALUNO_NAO_ENCONTRADO));

            var turmaExiste = await turmaRepository.GetByIdTurmaAsync(request.TurmaId, cancellationToken);
            if (turmaExiste == null)
                return Result.Invalid(new ValidationError(NomesErros.ERRO_TURMA_NAO_ENCONTRADA));

            var alunoNaTurma = await matriculaRepository.ExisteAlunoNaTurmaAsync(request.AlunoId, request.TurmaId, cancellationToken);
            if (alunoNaTurma)
                return Result.Invalid(new ValidationError("Matricula",NomesErros.ERRO_ALUNO_JA_MATRICULADO));

            var matricula = new Matricula(request.AlunoId, request.TurmaId);
            var result = await matriculaRepository.CreateMatriculaAsync(matricula, cancellationToken);
            if (!result)
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
     