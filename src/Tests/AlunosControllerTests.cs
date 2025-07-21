using Api.Controllers.v1;
using Application.Alunos.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ardalis.Result;
using Tests.Fixtures;
using Application.Alunos.Commands.Delete;
using Application.Alunos.Commands.Update;

namespace Tests;

public class AlunosControllerTests
{
    [Fact]
    public async Task GetAlunoByIdAynsc_DeveRetornarOk_QuandoAlunoExiste()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var alunoId = 1;
        var viewModel = AlunoFixtures.GetAlunoByIdViewModelMock(alunoId);
       
        mediatorMock.Setup(m => m.Send(It.IsAny<GetAlunoByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(viewModel);
        
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.GetAlunoByIdAynsc(mediatorMock.Object, alunoId, cancellationToken);

        // Assert
        var actionResult = Assert.IsType<ActionResult<GetAlunoByIdViewModel>>(result);
        Assert.NotNull(actionResult.Result);
    }

    [Fact]
    public async Task CreateAlunoAynsc_DeveRetornarOk_QuandoCriarAluno()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AlunoFixtures.GetValidCreateAlunoCommand();
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
        
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateAlunoAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateAlunoAynsc_DeveRetornarBadRequest_QuandoValidacaoFalhar()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AlunoFixtures.GetInvalidCreateAlunoCommand();
        var validationErrors = new[] { new ValidationError { Identifier = "Nome", ErrorMessage = "Nome é obrigatório" } };
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Invalid(validationErrors));
        
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateAlunoAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
       
        Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        Assert.NotNull(badRequest.Value);
    }

    [Fact]
    public async Task CreateAlunoAynsc_DeveRetornarErroInterno_QuandoErroOcorrer()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AlunoFixtures.GetValidCreateAlunoCommand();
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Error("Erro interno"));
        
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateAlunoAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.NotNull(objectResult.Value);
    }

    [Fact]
    public async Task DeleteAlunoAynsc_DeveRetornarOk_QuandoAlunoDeletado()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var id = 1;
        var command = AlunoFixtures.GetDeleteAlunoCommand(id);
       
        mediatorMock.Setup(m => m.Send(It.Is<DeleteAlunoCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
       
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.DeleteAlunoAynsc(mediatorMock.Object, id, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateAlunoAynsc_DeveRetornarOk_QuandoAlunoAtualizado()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AlunoFixtures.GetUpdateAlunoCommand();
        var viewModel = AlunoFixtures.GetUpdateAlunoViewModelMock(command.Id);
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(viewModel));
       
        var controller = new AlunosController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.UpdateAlunoAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okObject = Assert.IsType<ActionResult<UpdateAlunoViewModel>>(result);     
        Assert.NotNull(okObject.Value);
    }
}
