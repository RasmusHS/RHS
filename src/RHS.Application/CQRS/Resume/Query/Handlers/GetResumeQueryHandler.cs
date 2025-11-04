using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.CQRS.DTO.Resume.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Query.Handlers;

public class GetResumeQueryHandler : IQueryHandler<GetResumeQuery, QueryResumeDto>
{
    private readonly IResumeRepository _resumeRepository;
    private readonly IProjectRepository _projectRepository;
    
    public GetResumeQueryHandler(IResumeRepository resumeRepository, IProjectRepository projectRepository)
    {
        _resumeRepository = resumeRepository;
        _projectRepository = projectRepository;
    }
    
    public async Task<Result<QueryResumeDto>> Handle(GetResumeQuery query, CancellationToken cancellationToken = default)
    {
        var resumeResult = await _resumeRepository.GetByIdIncludeProjectsAsync(query.Id) ?? throw new KeyNotFoundException($"Resume with ID {query.Id} not found.");
        var projectsResult = await _projectRepository.GetAllByResumeIdAsync(query.Id) ?? throw new KeyNotFoundException($"Projects for Resume ID {query.Id} not found.");
        resumeResult.AddRangeProjects(projectsResult.ToList());

        var resumeDto = new QueryResumeDto(
            resumeResult.Id,
            resumeResult.Introduction,
            resumeResult.FullName.FirstName,
            resumeResult.FullName.LastName,
            resumeResult.Address.Street,
            resumeResult.Address.ZipCode,
            resumeResult.Address.City,
            resumeResult.Email.Value,
            resumeResult.GitHubLink,
            resumeResult.LinkedInLink,
            resumeResult.Photo,
            resumeResult.Projects.Select(p => new QueryProjectDto(
                p.Id,
                p.ResumeId,
                p.ProjectTitle,
                p.Description,
                p.ProjectUrl,
                p.DemoGif,
                p.IsFeatured,
                p.Created,
                p.LastModified)).ToList(),
            resumeResult.Created,
            resumeResult.LastModified);
        
        return Result.Ok(resumeDto);
    }
}