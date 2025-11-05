using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Project.Command;

public record UpdateProjectDto : DtoBase
{
    public UpdateProjectDto(ProjectId id, ResumeId resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured, DateTime created, DateTime lastModified)
    {
        Id = id;
        ResumeId = resumeId;
        
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public UpdateProjectDto() { }
    
    public ProjectId Id { get; set; }
    public ResumeId ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public byte[] DemoGif { get; set; }
    public bool IsFeatured { get; set; }
}