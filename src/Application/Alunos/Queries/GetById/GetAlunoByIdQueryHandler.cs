using Application.Alunos.Queries.GetAll;
using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Alunos.Queries.GetById;

public class GetAlunoByIdQueryHandler(ILogger<GetAllAlunosQueryHandler> logger, IAlunoRepository alunoRepository) : IRequestHandler<GetAlunoByIdQuery, Result<GetAlunoByIdViewModel>>
{
    public async Task<Result<GetAlunoByIdViewModel>> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var aluno = await alunoRepository.GetByIdAlunoAsync(request.Id, cancellationToken);
            if (aluno == null)
                return Result.NoContent();

            return Result.Success(AlunoExtensions.ToGetAlunoByIdViewModel(aluno));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_GET_ID_ALUNO, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_GET);
        }
    }
}
