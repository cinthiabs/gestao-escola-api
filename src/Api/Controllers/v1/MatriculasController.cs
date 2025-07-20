using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/matriculas")]
[Authorize]
public class MatriculasController : ApiController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateMatricula", Description = "Cria registro de matrícula")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateMatriculaAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreateMatriculaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return ActionResult(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetAllMatriculas", Description = "Obtém todos os registros de matrícula")]
    [ProducesResponseType(typeof(GetAllMatriculasPaginadoViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllMatriculasAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetAllMatriculaQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return ActionResult(result);
    }

}

