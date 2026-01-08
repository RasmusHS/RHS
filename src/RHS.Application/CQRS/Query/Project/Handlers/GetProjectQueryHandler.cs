using RHS.Application.CQRS.DTO.Project.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Query.Project.Handlers;

public class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, QueryProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<QueryProjectDto>> Handle(GetProjectQuery query, CancellationToken cancellationToken = default)
    {
        var projectResult = await _projectRepository.GetByIdAsync(query.Id) ?? throw new KeyNotFoundException($"Project with ID {query.Id} not found.");
        
        var projectDto = new QueryProjectDto(
            projectResult.Id.Value,
            projectResult.ResumeId.Value,
            projectResult.ProjectTitle,
            projectResult.Description,
            projectResult.ProjectUrl,
            projectResult.DemoGif,
            projectResult.IsFeatured,
            projectResult.Created,
            projectResult.LastModified);
        
        return Result.Ok(projectDto);
    }
}