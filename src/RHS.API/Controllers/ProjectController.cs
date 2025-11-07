using Microsoft.AspNetCore.Mvc;
using RHS.Application.Data;

namespace RHS.API.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : BaseController
{
    private IDispatcher _dispatcher;
    
    public ProjectController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}