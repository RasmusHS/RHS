using Helpers;
using Moq;
using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.CQRS.DTO.Resume.Query;
using RHS.Application.CQRS.Resume.Query;
using RHS.Application.CQRS.Resume.Query.Handlers;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.QueryHandlers;

[TestClass]
public class GetResumeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsOkResult_WhenResumeAndProjectsAreRetrievedSuccessfully()
    {
        // Arrange
        var resume = ResumeEntity.Create(
            "Intro",
            FullName.Create("John", "Doe").Value,
            Address.Create("123 St", "12345", "City").Value,
            Email.Create("test@example.com").Value,
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 });

        var projects = new List<ProjectEntity>
        {
            ProjectEntity.Create(resume.Value.Id, "Project 1", "Description 1", "https://project1.com", new byte[] { 4, 5, 6 }, true).Value,
            ProjectEntity.Create(resume.Value.Id, "Project 2", "Description 2", "https://project2.com", new byte[] { 7, 8, 9 }, true).Value
        };
        
        var query = new GetResumeQuery(resume.Value.Id);

        var resumeRepositoryMock = new Mock<IResumeRepository>();
        resumeRepositoryMock.Setup(repo => repo.GetByIdIncludeProjectsAsync(query.Id)).ReturnsAsync(resume.Value);

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetAllByResumeIdAsync(query.Id)).ReturnsAsync(projects);

        var handler = new GetResumeQueryHandler(resumeRepositoryMock.Object, projectRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(query);
        
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(query.Id, result.Value.Id);
        Assert.NotEmpty(result.Value.Projects);
    }
    
    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenResumeIsNotFound()
    {
        // Arrange
        var query = new GetResumeQuery(ResumeId.Create().Value);

        var resumeRepositoryMock = new Mock<IResumeRepository>();
        resumeRepositoryMock.Setup(repo => repo.GetByIdIncludeProjectsAsync(query.Id)).ReturnsAsync((ResumeEntity)null);

        var projectRepositoryMock = new Mock<IProjectRepository>();

        var handler = new GetResumeQueryHandler(resumeRepositoryMock.Object, projectRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query));
    }
    
    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenProjectsAreNotFound()
    {
        // Arrange
        var resume = ResumeEntity.Create(
            "Intro",
            FullName.Create("John", "Doe").Value,
            Address.Create("123 St", "12345", "City").Value,
            Email.Create("test@example.com").Value,
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 });

        var projects = new List<ProjectEntity>
        {
            ProjectEntity.Create(resume.Value.Id, "Project 1", "Description 1", "https://project1.com", new byte[] { 4, 5, 6 }, true).Value,
            ProjectEntity.Create(resume.Value.Id, "Project 2", "Description 2", "https://project2.com", new byte[] { 7, 8, 9 }, true).Value
        };
        
        var query = new GetResumeQuery(ResumeId.Create().Value);

        var resumeRepositoryMock = new Mock<IResumeRepository>();
        resumeRepositoryMock.Setup(repo => repo.GetByIdIncludeProjectsAsync(query.Id)).ReturnsAsync(resume.Value);

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetAllByResumeIdAsync(query.Id)).ReturnsAsync((List<ProjectEntity>)null);

        var handler = new GetResumeQueryHandler(resumeRepositoryMock.Object, projectRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query));
    }
}