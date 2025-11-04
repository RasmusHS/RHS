using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.CQRS.Resume.Project.Command.Handlers;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.Project.CommandHandlers;

[TestClass]
public class DeleteProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsOkResult_WhenProjectIsDeletedSuccessfully()
    {
        // Arrange
        var command = new DeleteProjectCommand(ProjectId.Create().Value);

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var handler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        projectRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ThrowsException_WhenDeleteFails()
    {
        // Arrange
        var command = new DeleteProjectCommand(ProjectId.Create().Value);

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Delete failed"));
        
        var handler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);
        
        // Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        projectRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
}