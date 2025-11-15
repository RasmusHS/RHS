using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RHS.Application.CQRS.DTO.Resume.Command;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Application.CQRS.DTO.Resume.Query;
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
    public async Task GetResumeById_WithNonExistingId_ReturnsBadRequest()
    {
        // Arrange
        //var query = new QueryResumeDto();
        
        var nonExistingId = ResumeId.Create().Value;
        //var response = await _client.GetFromJsonAsync($"/api/resume/{nonExistingId}", typeof(ResumeId));
        var response = await _client.GetAsync($"/api/resume/{nonExistingId}");
        _output.WriteLine(response.Content.ReadAsStringAsync().Result);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
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