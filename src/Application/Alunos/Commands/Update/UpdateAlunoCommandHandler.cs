using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Alunos.Commands.Update;

public class UpdateAlunoCommandHandler(ILogger<UpdateAlunoCommandHandler> logger, IValidator<UpdateAlunoCommand> validator, IAlunoRepository alunoRepository) : IRequestHandler<UpdateAlunoCommand, Result<UpdateAlunoViewModel>>
{
    public async Task<Result<UpdateAlunoViewModel>> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorCommand = validator.Validate(request);
            
            var result = ValidationResultExtensions.ToResult(validatorCommand);
            if (!result.IsSuccess)
                return Result.Invalid(result.ValidationErrors);

            var aluno= await alunoRepository.GetByIdAlunoAsync(request.Id, cancellationToken);
            if (aluno == null)
                return Result.NoContent();

            aluno.Atualizar(
                nome: request.Nome,
                dataNascimento: request.DataNascimento,
                cpf: request.CPF,
                email: request.Email,
                senha: request.Senha);

            var updateAluno = await alunoRepository.UpdateAlunoAsync(aluno, cancellationToken);
            if (!updateAluno)
                return Result.Error(NomesErros.ERROR_UPDATE);

            var getAlunoAtualizado = await alunoRepository.GetByIdAlunoAsync(request.Id, cancellationToken);

            return Result.Success(AlunoExtensions.ToUpdateAlunoViewModel(getAlunoAtualizado));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Logs.LOG_ERROR, NomesFluxos.FLUXO_UPDATE_ALUNO, ex.Message);
            return Result.CriticalError(NomesErros.ERROR_UPDATE);
        }
    }
}