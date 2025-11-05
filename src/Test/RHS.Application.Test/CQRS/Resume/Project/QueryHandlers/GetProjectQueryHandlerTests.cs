using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Project.Query;
using RHS.Application.CQRS.Resume.Project.Query.Handlers;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.Project.QueryHandlers;

[TestClass]
public class GetProjectQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsQueryProjectDto_WhenProjectExists()
    {
        // Arrange
        var project = ProjectEntity.Create(
            ResumeId.Create().Value, 
            "Sample Project",
            "Sample Description",
            "https://example.com",
            new byte[] { 0, 1, 2 },
            true).Value;
        
        var query = new GetProjectQuery(project.Id);
        
        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetByIdAsync(query.Id)).ReturnsAsync(project);
        var handler = new GetProjectQueryHandler(projectRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(project.Id, result.Value.Id);
        Assert.Equal(project.ResumeId, result.Value.ResumeId);
        Assert.Equal(project.ProjectTitle, result.Value.ProjectTitle);
        Assert.Equal(project.Description, result.Value.Description);
        Assert.Equal(project.ProjectUrl, result.Value.ProjectUrl);
        Assert.Equal(project.DemoGif, result.Value.DemoGif);
        Assert.Equal(project.IsFeatured, result.Value.IsFeatured);
        Assert.Equal(project.Created, result.Value.Created);
        Assert.Equal(project.LastModified, result.Value.LastModified);
    }
    
    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenProjectDoesNotExist()
    {
        // Arrange
        var project = ProjectEntity.Create(
            ResumeId.Create().Value, 
            "Sample Project",
            "Sample Description",
            "https://example.com",
            new byte[] { 0, 1, 2 },
            true).Value;
        var query = new GetProjectQuery(project.Id);

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetByIdAsync(query.Id)).ReturnsAsync((ProjectEntity)null);
        var _handler = new GetProjectQueryHandler(projectRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query));
    }
}