using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RHS.Application.CQRS.DTO.Resume.Command;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.API.IntegrationTests;

public class ResumeControllerTests : BaseIntegrationTest
{
    private readonly HttpClient _client;
    
    public ResumeControllerTests(RHSWebApplicationFactory factory) : base(factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateResume_WithValidRequestAndProjects_ReturnsOkAndSuccessIndicated()
    {
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

        var response = await _client.PostAsJsonAsync("/api/resume/createResume", payload);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        bool success = TryGetBoolean(json, "success", out var s) ? s : TryGetBoolean(json, "Success", out var s2) ? s2 : false;
        Assert.True(success);
    }
    
    [Fact]
    public async Task CreateResume_WithValidRequestWithoutProjects_ReturnsOkAndSuccessIndicated()
    {
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
            null);

        var response = await _client.PostAsJsonAsync("/api/resume/createResume", payload);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        bool success = TryGetBoolean(json, "success", out var s) ? s : TryGetBoolean(json, "Success", out var s2) ? s2 : false;
        Assert.True(success);
    }
    
    [Fact]
    public async Task CreateResume_WithInvalidModel_ReturnsBadRequest()
    {
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

        var response = await _client.PostAsJsonAsync("/api/resume/createResume", invalidPayload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResumeById_WithNonExistingId_ReturnsBadRequest()
    {
        var nonExistingId = ResumeId.Create();
        var response = await _client.GetAsync($"/api/resume/{nonExistingId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    private static bool TryGetBoolean(JsonElement element, string propertyName, out bool value)
    {
        value = false;
        if (element.ValueKind != JsonValueKind.Object) return false;
        if (!element.TryGetProperty(propertyName, out var prop)) return false;
        if (prop.ValueKind != JsonValueKind.True && prop.ValueKind != JsonValueKind.False) return false;
        value = prop.GetBoolean();
        return true;
    }
}