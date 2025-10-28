using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Project.Query;

public class QueryProjectDto : DtoBase
{
    public QueryProjectDto(ProjectId id, ResumeId resumeId, string projectTitle, string description, string? projectUrl, DateTime created, DateTime lastModified)
    {
        Id = id;
        ResumeId = resumeId;
        
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryProjectDto() { }
    
    public ProjectId Id { get; set; }
    public ResumeId ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string? ProjectUrl { get; set; }
}