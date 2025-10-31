using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Project.Command;

public record CreateProjectDto
{
    public CreateProjectDto(ResumeId resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        ResumeId = resumeId;
        
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
    }

    public CreateProjectDto() { }
    
    public ResumeId ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public byte[] DemoGif { get; set; }
    public bool IsFeatured { get; set; }
}