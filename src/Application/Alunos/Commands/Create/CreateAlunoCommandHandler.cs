using Application.Extensions;
using Ardalis.Result;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Alunos.Commands.Create;

public class CreateAlunoCommandHandler(ILogger<CreateAlunoCommandHandler> logger, IValidator<CreateAlunoCommand> validator, IAlunoRepository alunoRepository) : IRequestHandler<CreateAlunoCommand, Result>
{
    public async Task<Result> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validatorCommand = validator.Validate(request);
            var result = ValidationResultExtensions.ToResult(validatorCommand);
            if (!result.IsSuccess)
                return Result.Invalid(result.ValidationErrors);

            var aluno = Aluno.Criar(request.Nome, request.DataNascimento, request.CPF, request.Email, request.Senha);

            var existeAluno = await alunoRepository.ExisteAlunoRegistrado(aluno.CPF, aluno.Email, cancellationToken);
            if (existeAluno)
                return Result.Invalid(new ValidationError(NomesErros.CONFLITO));

            var criaNovoAluno = await alunoRepository.CreateAlunoAsync(aluno, cancellationToken);
            if (!criaNovoAluno)
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