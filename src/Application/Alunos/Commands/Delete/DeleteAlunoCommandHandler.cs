using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Alunos.Commands.Delete;

public class DeleteAlunoCommandHandler(ILogger<DeleteAlunoCommandHandler> logger, IAlunoRepository alunoRepository) : IRequestHandler<DeleteAlunoCommand, Result>
{
    public async Task<Result> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getAlunoById = await alunoRepository.GetByIdAlunoAsync(request.Id, cancellationToken);
            if (getAlunoById == null)
                return Result.Invalid(new ValidationError());
             
            var deleteAluno = await alunoRepository.DeleteAlunoAsync(request.Id, cancellationToken);
            if (!deleteAluno)
                return Result.Error(NomesErros.ERROR_DELETE);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_DELETE_ALUNO, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_DELETE);
        }
    }
}

