using Api.Controllers.v1;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Fixtures;

namespace Tests;

public class MatriculasControllerTests
{
    [Fact]
    public async Task CreateMatriculaAsync_DeveRetornarOk_QuandoMatriculaCriada()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = MatriculaFixtures.GetValidCreateMatriculaCommand();
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
       
        var controller = new MatriculasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateMatriculaAsync(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateMatriculaAsync_DeveRetornarBadRequest_QuandoValidacaoFalhar()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = MatriculaFixtures.GetInvalidCreateMatriculaCommand();
        var validationErrors = new[] { new ValidationError { Identifier = "AlunoId", ErrorMessage = "AlunoId é obrigatório" } };
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Invalid(validationErrors));
       
        var controller = new MatriculasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateMatriculaAsync(mediatorMock.Object, command, cancellationToken);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        Assert.NotNull(badRequest.Value);
    }

    [Fact]
    public async Task GetAllMatriculasAsync_DeveRetornarOk_QuandoMatriculasExistem()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var query = MatriculaFixtures.GetAllMatriculaQueryMock();
        var viewModel = MatriculaFixtures.GetAllMatriculasPaginadoViewModelMock();
       
        mediatorMock.Setup(m => m.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(viewModel);
       
        var controller = new MatriculasController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.GetAllMatriculasAsync(mediatorMock.Object, query, cancellationToken);

        // Assert
        var okObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
        Assert.NotNull(okObject.Value);
    }
}
