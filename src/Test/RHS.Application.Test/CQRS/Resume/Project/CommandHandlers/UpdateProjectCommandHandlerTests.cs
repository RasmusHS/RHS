using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.CQRS.Resume.Project.Command.Handlers;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.Project.CommandHandlers;

[TestClass]
public class UpdateProjectCommandHandlerTests
{ 
    [Fact]
    public async Task Handle_ReturnsOkResult_WhenProjectIsUpdatedSuccessfully()
    {
        // Arrange
        var command = new UpdateProjectCommand
        (
            ProjectId.Create().Value,
            ResumeId.Create().Value,
            "Updated Title",
            "Updated Description",
            "https://updated-url.com",
            new byte[] { 1, 2, 3 },
            true,
            DateTime.Now, 
            DateTime.Now
        );

        var project = ProjectEntity.Create(ResumeId.Create().Value, "Old Title", "Old Description", "https://old-url.com", new byte[] { 0 }, false);
        var projectRepositoryMock = new Mock<IProjectRepository>();
    
        projectRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(project.Value);
        var handler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.Success);
        projectRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<ProjectEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenProjectDoesNotExist()
    {
        // Arrange
        var command = new UpdateProjectCommand
        (
            ProjectId.Create().Value,
            ResumeId.Create().Value,
            "Updated Title",
            "Updated Description",
            "https://updated-url.com",
            new byte[] { 1, 2, 3 },
            true,
            DateTime.Now, 
            DateTime.Now
        );

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((ProjectEntity)null);

        var handler = new UpdateProjectCommandHandler(projectRepositoryMock.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command));
    }
}