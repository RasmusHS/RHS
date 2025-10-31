using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Resume.Project.Command;

public class CreateProjectCommand : ICommand
{
    public CreateProjectCommand(ResumeId resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        ResumeId = resumeId;
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
    }
    
    public ResumeId ResumeId { get; set; }
    public string ProjectTitle { get; }
    public string Description { get; }
    public string ProjectUrl { get; }
    public byte[] DemoGif { get; }
    public bool IsFeatured { get; }
}