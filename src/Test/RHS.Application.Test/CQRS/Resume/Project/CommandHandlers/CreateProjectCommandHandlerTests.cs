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
public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProjectIsCreatedSuccessfully()
    {
        // Arrange
        var command = new CreateProjectCommand
        (
            ResumeId.Create().Value,
            "Project Title",
            "Project Description",
            "https://example.com",
            new byte[] { 1, 2, 3 },
            true
        );

        var projectRepositoryMock = new Mock<IProjectRepository>();
        var handler = new CreateProjectCommandHandler(projectRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        projectRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ProjectEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}