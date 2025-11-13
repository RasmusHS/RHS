using Microsoft.AspNetCore.Mvc.Testing;

namespace RHS.API.IntegrationTests;

public class ProjectControllerTests : BaseIntegrationTest
{
    private readonly HttpClient _client;
    
    public ProjectControllerTests(RHSWebApplicationFactory factory) : base(factory)
    {
        _client = factory.CreateClient();
    }
}