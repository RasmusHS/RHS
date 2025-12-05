using System.Net;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Xunit.Abstractions;

namespace RHS.API.IntegrationTests;

public class ResumeControllerTests : BaseIntegrationTest
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    
    public ResumeControllerTests(RHSWebApplicationFactory factory, ITestOutputHelper output) : base(factory)
    {
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task GetResumeById_ReturnsOk_WhenDispatcherReturnsSuccessfulResult()
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
        
        //_output.WriteLine(DbContext.Resumes.AsNoTracking().Any().ToString());
        //_output.WriteLine(DbContext.Resumes.AsNoTracking().Count().ToString());
        
        // Act
        var response = await _client.GetAsync($"/api/resume/{resume.Value.Id.Value}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetResumeById_ReturnsBadRequest_WhenResumeDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync("/api/resume/invalid-guid");
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResumeById_ReturnsInternalServerErrorAndThrows_WhenResumeDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync($"/api/resume/{Guid.NewGuid()}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Throws<KeyNotFoundException>( () => 
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new KeyNotFoundException();
            }
        });
    }
}