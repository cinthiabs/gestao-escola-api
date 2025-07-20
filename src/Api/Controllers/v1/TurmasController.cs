using Application.Turmas.Commands.Create;
using Application.Turmas.Commands.Delete;
using Application.Turmas.Commands.Update;
using Application.Turmas.Queries.GetAll;
using Application.Turmas.Queries.GetById;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/turmas")]
[Authorize]
public class TurmasController : ApiController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateTurma", Description = "Cria registro de turma")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateTurmaAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreateTurmaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return ActionResult(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetAllTurmas", Description = "Obtém todas as turmas cadastradas paginadas")]
    [ProducesResponseType(typeof(GetAllTurmasPaginadoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetAllTurmasPaginadoViewModel>> GetAllTurmasAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken,
        [FromQuery] GetAllTurmasQuery query)
    {
        var result = await mediator.Send(query, cancellationToken);
        return ActionResult(result);
    }


    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "GetTurmaById", Description = "Obtém turma cadastrada por ID")]
    [ProducesResponseType(typeof(GetTurmaByIdViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GetTurmaByIdViewModel>> GetTurmaByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTurmaByIdQuery(id), cancellationToken);
        return ActionResult(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "DeleteTurma", Description = "Deleta turma cadastrada por ID")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTurmaAsync(
         [FromServices] IMediator mediator,
         [FromRoute] int id,
         CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteTurmaCommand(id), cancellationToken);
        return ActionResult(result);
    }

    [HttpPut]
    [SwaggerOperation(OperationId = "UpdateTurma", Description = "Atualiza turma cadastrada")]
    [ProducesResponseType(typeof(UpdateTurmaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UpdateTurmaViewModel>> UpdateTurmaAsync(
        [FromServices] IMediator mediator,
        [FromBody] UpdateTurmaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return ActionResult(result);
    }
}
