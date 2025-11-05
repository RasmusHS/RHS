using Helpers;
using Moq;
using RHS.Application.CQRS.Resume.Project.Query;
using RHS.Application.CQRS.Resume.Project.Query.Handlers;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Application.Test.CQRS.Resume.Project.QueryHandlers;

[TestClass]
public class GetAllProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCollectionResponseWithProjects_WhenProjectsExist()
    {
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
        var query = new GetAllProjectsQuery(resume.Value.Id);
        
        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetAllByResumeIdAsync(query.ResumeId)).ReturnsAsync(projects);
        var handler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

        var result = await handler.Handle(query);

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Data.Count());
        Assert.Equal("Project 1", result.Value.Data.First().ProjectTitle);
        Assert.Equal("Project 2", result.Value.Data.Last().ProjectTitle);
    }
    
    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenNoProjectsExistForResumeId()
    {
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
        var query = new GetAllProjectsQuery(resume.Value.Id);
        
        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(repo => repo.GetAllByResumeIdAsync(query.ResumeId)).ReturnsAsync((List<ProjectEntity>)null);
        var handler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query));
    }
}