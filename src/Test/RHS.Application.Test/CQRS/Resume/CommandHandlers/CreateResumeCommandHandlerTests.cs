using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Command;
using RHS.Application.CQRS.Resume.Command.Handlers;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.CommandHandlers;

[TestClass]
public class CreateResumeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        // Arrange
        var command = new CreateResumeCommand
        (
            "Introduction text",
            "John",
            "Doe",
            "123 Street",
            "12345",
            "City",
            "test@example.com",
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 },
            new List<CreateProjectCommand>
            {
                new CreateProjectCommand
                (
                    ResumeId.Create().Value, 
                    "Project 1",
                    "Description 1",
                    "https://project1.com",
                    new byte[] { 4, 5, 6 },
                    true
                )
            }
        );
        
        var resumeRepositoryMock = new Mock<IResumeRepository>();
        var handler = new CreateResumeCommandHandler(resumeRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        resumeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ResumeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNoProjectsProvided()
    {
        // Arrange
        var command = new CreateResumeCommand
        (
            "Introduction text",
            "John",
            "Doe",
            "123 Street",
            "12345",
            "City",
            "test@example.com",
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 },
            new List<CreateProjectCommand> { }
        );
        
        var resumeRepositoryMock = new Mock<IResumeRepository>();
        var handler = new CreateResumeCommandHandler(resumeRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        resumeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ResumeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}