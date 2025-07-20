using Application.Alunos.Commands.Create;
using Application.Alunos.Commands.Delete;
using Application.Alunos.Commands.Update;
using Application.Alunos.Queries.GetAll;
using Application.Alunos.Queries.GetById;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/alunos")]
[Authorize]
public class AlunosController : ApiController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateAluno", Description = "Cria registro de aluno")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAlunoAynsc(
        [FromServices] IMediator mediator,
        [FromBody] CreateAlunoCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return ActionResult(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetAllAlunos", Description = "Obtém todos os alunos cadastrados paginados")]
    [ProducesResponseType(typeof(GetAllAlunosPaginadoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetAllAlunosPaginadoViewModel>> GetAllAlunosAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken,
        [FromQuery] GetAllAlunosQuery query)
    {
        var result = await mediator.Send(query, cancellationToken);
        return ActionResult(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "GetAlunoById", Description = "Obtém aluno cadastrado por ID")]
    [ProducesResponseType(typeof(GetAlunoByIdViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetAlunoByIdViewModel>> GetAlunoByIdAynsc(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAlunoByIdQuery(id), cancellationToken);
        return ActionResult(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "DeleteAluno", Description = "Deleta aluno cadastrado por ID")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAlunoAynsc(
         [FromServices] IMediator mediator,
         [FromRoute] int id,
         CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteAlunoCommand(id), cancellationToken);
        return ActionResult(result);
    }

    [HttpPut]
    [SwaggerOperation(OperationId = "UpdateAluno", Description = "Atualiza aluno cadastrado")]
    [ProducesResponseType(typeof(UpdateAlunoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UpdateAlunoViewModel>> UpdateAlunoAynsc(
    [FromServices] IMediator mediator,
    [FromBody] UpdateAlunoCommand command,
    CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return ActionResult(result);
    }

}
