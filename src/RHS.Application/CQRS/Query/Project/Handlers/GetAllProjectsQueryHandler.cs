using RHS.Application.CQRS.DTO.Project.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Query.Project.Handlers;

public class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, CollectionResponseBase<QueryProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result<CollectionResponseBase<QueryProjectDto>>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken = default)
    {
        List<QueryProjectDto> projects = new List<QueryProjectDto>();
        var projectsResult = await _projectRepository.GetAllByResumeIdAsync(query.ResumeId) ?? throw new KeyNotFoundException($"Projects for Resume ID {query.ResumeId} not found.");
        if (projectsResult.Count() < 1)
        {
            throw new KeyNotFoundException($"Projects for Resume ID {query.ResumeId} not found.");
        }

        foreach (var project in projectsResult)
        {
            QueryProjectDto dto = new QueryProjectDto(
                project.Id, 
                project.ResumeId, 
                project.ProjectTitle, 
                project.Description, 
                project.ProjectUrl, 
                project.DemoGif, 
                project.IsFeatured, 
                project.Created, 
                project.LastModified
                );
            
            projects.Add(dto);
        }
        return new CollectionResponseBase<QueryProjectDto>()
        {
            Data = projects
        };
    }
}