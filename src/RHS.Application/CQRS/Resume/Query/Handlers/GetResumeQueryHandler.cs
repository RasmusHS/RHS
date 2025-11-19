using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.CQRS.DTO.Resume.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Query.Handlers;

public class GetResumeQueryHandler : IQueryHandler<GetResumeQuery, QueryResumeDto>
{
    private readonly IResumeRepository _resumeRepository;
    
    public GetResumeQueryHandler(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }
    
    public async Task<Result<QueryResumeDto>> Handle(GetResumeQuery query, CancellationToken cancellationToken = default)
    {
        var resumeResult = await _resumeRepository.GetByIdIncludeProjectsAsync(query.Id) ?? throw new KeyNotFoundException($"Resume with ID {query.Id} not found.");

        var resumeDto = new QueryResumeDto(
            resumeResult.Id.Value,
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