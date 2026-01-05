using System.Net;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Xunit.Abstractions;

namespace RHS.API.IntegrationTests;

public class ProjectControllerTests : BaseIntegrationTest
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    
    public ProjectControllerTests(RHSWebApplicationFactory factory, ITestOutputHelper output) : base(factory)
    {
        _client = factory.CreateClient();
        _output = output;
    }
    
    [Fact]
    public async Task GetProject_ReturnsOk_WhenProjectExists()
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
        
        var project = ProjectEntity.Create(resume.Value.Id, "Project 1", "Description 1", "https://project1.com", new byte[] { 4, 5, 6 }, true).Value;
        
        await DbContext.Database.BeginTransactionAsync();
        await DbContext.Resumes.AddAsync(resume.Value);
        await DbContext.Projects.AddAsync(project);
        await DbContext.SaveChangesAsync();
        await DbContext.Database.CommitTransactionAsync();
        
        // Act
        var response = await _client.GetAsync($"/api/project/getProject/{project.Id.Value}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetProject_ReturnsInternalServerErrorAndThrows_WhenProjectDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync($"/api/project/getProject/{Guid.NewGuid()}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("not found", responseBody, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public async Task GetProjectsByResumeId_ReturnsOk_WhenProjectsExist()
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
        
        await DbContext.Database.BeginTransactionAsync();
        await DbContext.Resumes.AddAsync(resume.Value);
        await DbContext.Projects.AddRangeAsync(projects);
        await DbContext.SaveChangesAsync();
        await DbContext.Database.CommitTransactionAsync();
        
        // Act
        var response = await _client.GetAsync($"/api/project/{resume.Value.Id.Value}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetProjectsByResumeId_ReturnsInternalServerErrorAndThrows_WhenNoProjectsExist()
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
        
        await DbContext.Database.BeginTransactionAsync();
        await DbContext.Resumes.AddAsync(resume.Value);
        await DbContext.SaveChangesAsync();
        await DbContext.Database.CommitTransactionAsync();
        
        // Act
        var response = await _client.GetAsync($"/api/project/{resume.Value.Id.Value}");
        var responseBody = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseBody);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("not found", responseBody, StringComparison.OrdinalIgnoreCase);
    }
}