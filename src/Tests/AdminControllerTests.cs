using Api.Controllers.v1;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Fixtures;

namespace Tests;

public class AdminControllerTests
{
    [Fact]
    public async Task CreateAdminAynsc_DeveRetornarOk_QuandoAdminCriado()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AdminFixtures.GetValidCreateAdminCommand();
        
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
       
        var controller = new AdminController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateAdminAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateAdminAynsc_DeveRetornarBadRequest_QuandoValidacaoFalhar()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var command = AdminFixtures.GetInvalidCreateAdminCommand();
       
        var validationErrors = new[] { new ValidationError { Identifier = "Email", ErrorMessage = "Email é obrigatório" } };
        mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Invalid(validationErrors));
        
        var controller = new AdminController();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.CreateAdminAynsc(mediatorMock.Object, command, cancellationToken);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        Assert.NotNull(badRequest.Value);
    }
}
