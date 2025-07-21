using Api.Controllers.v1;
using Application.Turmas.Commands.Delete;
using Application.Turmas.Commands.Update;
using Application.Turmas.Queries.GetById;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Fixtures;

namespace Tests;

public class TurmasControllerTests
{
    [Fact]
    public async Task CreateTurmaAsync_DeveRetornarOk_QuandoTurmaCriada()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = TurmaFixtures.GetValidCreateTurmaCommand();
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
        
        var controller = new TurmasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateTurmaAsync(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateTurmaAsync_DeveRetornarBadRequest_QuandoValidacaoFalhar()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = TurmaFixtures.GetInvalidCreateTurmaCommand();
        var validationErrors = new[] { new ValidationError { Identifier = "Nome", ErrorMessage = "Nome é obrigatório" } };
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Invalid(validationErrors));
        
        var controller = new TurmasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateTurmaAsync(mediatorMock.Object, command, cancellationToken);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
       Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        Assert.NotNull(badRequest.Value);
    }

    [Fact]
    public async Task DeleteTurmaAsync_DeveRetornarOk_QuandoTurmaDeletada()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var id = 1;
        var command = TurmaFixtures.GetDeleteTurmaCommand(id);
        
        mediatorMock.Setup(m => m.Send(It.Is<DeleteTurmaCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
        
        var controller = new TurmasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.DeleteTurmaAsync(mediatorMock.Object, id, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
       
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateTurmaAsync_DeveRetornarOk_QuandoTurmaAtualizada()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = TurmaFixtures.GetUpdateTurmaCommand();
        var viewModel = TurmaFixtures.GetUpdateTurmaViewModelMock(command.Id);
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(viewModel));
        
        var controller = new TurmasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.UpdateTurmaAsync(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okObject = Assert.IsType<ActionResult<UpdateTurmaViewModel>>(result);
        Assert.NotNull(okObject.Value);
    }

    [Fact]
    public async Task GetTurmaByIdAsync_DeveRetornarOk_QuandoTurmaExiste()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var turmaId = 1;
        var viewModel = TurmaFixtures.GetTurmaByIdViewModelMock(turmaId);
        
        mediatorMock.Setup(m => m.Send(It.IsAny<GetTurmaByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(viewModel);
        
        var controller = new TurmasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.GetTurmaByIdAsync(mediatorMock.Object, turmaId, cancellationToken);

        // Assert
        var actionResult = Assert.IsType<ActionResult<GetTurmaByIdViewModel>>(result);
        Assert.NotNull(actionResult.Result);
    }
}
