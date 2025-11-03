using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Command;
using RHS.Application.CQRS.Resume.Command.Handlers;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.CommandHandlers;

[TestClass]
public class UpdateResumeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateResumeAndAddProjects_WhenProjectsAreProvided()
    {
        // Arrange
        var command = new UpdateResumeCommand
        (
            ResumeId.Create().Value,
            "Updated Introduction",
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

        var resume = ResumeEntity.Create("Introduction", FullName.Create("Jane", "Doe").Value, Address.Create("456 Avenue", "67890", "City").Value, Email.Create("old@example.com").Value, "https://github.com/janedoe", "https://linkedin.com/in/janedoe", new byte[] { 7, 8, 9 }).Value;
        var resumeRepositoryMock = new Mock<IResumeRepository>();
        var projectRepositoryMock = new Mock<IProjectRepository>();

        resumeRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(Result.Ok(resume));
        var handler = new UpdateResumeCommandHandler(resumeRepositoryMock.Object, projectRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        resumeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ResumeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        projectRepositoryMock.Verify(p => p.AddRangeAsync(It.IsAny<List<ProjectEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
        projectRepositoryMock.Verify(p => p.Save(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldUpdateResumeOnly_WhenNoProjectsAreProvided()
    {
        // Arrange
        var command = new UpdateResumeCommand
        (
            ResumeId.Create().Value,
            "Updated Introduction",
            "John",
            "Doe",
            "123 Street",
            "12345",
            "City",
            "test@example.com",
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 },
            new List<CreateProjectCommand>()
        );

        var resume = ResumeEntity.Create("Introduction", FullName.Create("Jane", "Doe").Value, Address.Create("456 Avenue", "67890", "City").Value, Email.Create("old@example.com").Value, "https://github.com/janedoe", "https://linkedin.com/in/janedoe", new byte[] { 7, 8, 9 }).Value;
        var resumeRepositoryMock = new Mock<IResumeRepository>();
        var projectRepositoryMock = new Mock<IProjectRepository>();

        resumeRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(Result.Ok(resume));
        var handler = new UpdateResumeCommandHandler(resumeRepositoryMock.Object, projectRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command);
        
        // Assert
        Assert.True(result.Success);
        resumeRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ResumeEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        projectRepositoryMock.Verify(p => p.AddRangeAsync(It.IsAny<List<ProjectEntity>>(), It.IsAny<CancellationToken>()), Times.Never);
        resumeRepositoryMock.Verify(r => r.Save(It.IsAny<CancellationToken>()), Times.Once);
    }
}

