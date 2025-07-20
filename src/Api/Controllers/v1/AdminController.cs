using Application.Admin.Commands.Auth;
using Application.Admin.Commands.Create;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/admins")]
    public class AdminController : ApiController
    {
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateAdmin", Description = "Criar registro de administrador")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAdminAynsc(
           [FromServices] IMediator mediator,
           [FromBody] CreateAdminCommand command,
           CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return ActionResult(result);
        }

        [HttpPost("autentica")]
        [SwaggerOperation(OperationId = "AuthAdmin", Description = "Autenticar administrador")]
        [ProducesResponseType(typeof(AuthAdminViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthAdminViewModel>> AuthAdminAsync(
           [FromServices] IMediator mediator,
           [FromBody] AuthAdminCommand command,
           CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return ActionResult(result);
        }


    }
}
