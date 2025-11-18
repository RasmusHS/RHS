using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using RHS.Application.CQRS.DTO.Resume.Command;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Application.CQRS.DTO.Resume.Query;
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
    public async Task CreateResume_WithValidRequestAndProjects_ReturnsOkAndSuccessIndicated()
    {
        // Arrange
        var payload = new CreateResumeDto(
            "Intro",
            "John",
            "Doe",
            "123 Main St",
            "12345",
            "City",
            "test@example.com",
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 },
            new List<CreateProjectDto>
            {
                new CreateProjectDto(
                    null,
                    "Project 1",
                    "Description 1",
                    "https://project1.example",
                    new byte[] { 4, 5, 6 },
                    true)
            });
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/resume/createResume", payload);
        _output.WriteLine(response.Content.ReadAsStringAsync().Result);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        _output.WriteLine(json.ToString());
        bool success = false;
        if (json.TryGetProperty("result", out var result) &&
            TryGetBoolean(result, "success", out var s))
        {
            success = s;
        }
        
        Assert.True(success);
    }
    
    [Fact]
    public async Task CreateResume_WithValidRequestWithoutProjects_ReturnsOkAndSuccessIndicated()
    {
        // Arrange
        var payload = new CreateResumeDto(
            "Intro",
            "John",
            "Doe",
            "123 Main St",
            "12345",
            "City",
            "test@example.com",
            "https://github.com/johndoe",
            "https://linkedin.com/in/johndoe",
            new byte[] { 1, 2, 3 });
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/resume/createResume", payload);
        _output.WriteLine(response.Content.ReadAsStringAsync().Result);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        _output.WriteLine(json.ToString());
        bool success = false;
        if (json.TryGetProperty("result", out var result) &&
            TryGetBoolean(result, "success", out var s))
        {
            success = s;
        }
        Assert.True(success);
    }
    
    [Fact]
    public async Task CreateResume_WithInvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var invalidPayload = new CreateResumeDto(
            "",
            "",
            "",
            "",
            "",
            "",
            "not-an-email",
            "",
            "",
            new byte[] { 1, 2, 3 },
            null);
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/resume/createResume", invalidPayload);
        _output.WriteLine(response.Content.ReadAsStringAsync().Result);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
        //await DbContext.AddAsync(resume.Value);
        await DbContext.Projects.AddRangeAsync(projects);
        await DbContext.SaveChangesAsync();
        await DbContext.Database.CommitTransactionAsync();
        
        _output.WriteLine(DbContext.Resumes.AsNoTracking().Any().ToString());
        _output.WriteLine(DbContext.Resumes.AsNoTracking().Count().ToString());
        
        // Act
        //var response = await _client.GetFromJsonAsync<QueryResumeDto>($"/api/resume/{resume.Value.Id.Value}");
        var response = await _client.GetAsync($"/api/resume/{resume.Value.Id.Value}");
        var responseBody = await response.Content.ReadAsStringAsync(); // log full server error for debugging
        _output.WriteLine(responseBody);
        //_output.WriteLine(response.ToString());
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //Assert.NotEmpty(response.Projects);
    }
    
    // [Fact]
    // public async Task GetResumeById_WithNonExistingId_ReturnsBadRequest()
    // {
    //     // Arrange
    //     // var arrangeDbItem = new CreateResumeDto(
    //     //     "Intro",
    //     //     "John",
    //     //     "Doe",
    //     //     "123 Main St",
    //     //     "12345",
    //     //     "City",
    //     //     "test@example.com",
    //     //     "https://github.com/johndoe",
    //     //     "https://linkedin.com/in/johndoe",
    //     //     new byte[] { 1, 2, 3 },
    //     //     new List<CreateProjectDto>
    //     //     {
    //     //         new CreateProjectDto(
    //     //             null,
    //     //             "Project 1",
    //     //             "Description 1",
    //     //             "https://project1.example",
    //     //             new byte[] { 4, 5, 6 },
    //     //             true)
    //     //     });
    //     
    //     var nonExistingId = ResumeId.Create().Value;
    //     //var response = await _client.GetAsync($"/api/resume/{nonExistingId.Value}");
    //     var response = await _client.GetFromJsonAsync<QueryResumeDto>($"/api/resume/{nonExistingId.Value}");
    //     //_output.WriteLine(response.Content.ReadAsStringAsync().Result);
    //     _output.WriteLine(response.ToString());
    //
    //     // Assert
    //     Assert.Throws<KeyNotFoundException>(() => 
    //     {
    //         if (response == null)
    //         {
    //             throw new KeyNotFoundException($"Resume with ID {nonExistingId} not found.");
    //         }
    //     });
    //     //Assert.Equal(HttpStatusCode.BadRequest, response);
    // }
    
    
    
    private static bool TryGetBoolean(JsonElement element, string propertyName, out bool value)
    {
        value = false;
        if (element.ValueKind != JsonValueKind.Object) return false;

        foreach (var prop in element.EnumerateObject())
        {
            if (string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                if (prop.Value.ValueKind != JsonValueKind.True && prop.Value.ValueKind != JsonValueKind.False)
                {
                    return false;
                }

                value = prop.Value.GetBoolean();
                return true;
            }
        }

        return false;
    }
}