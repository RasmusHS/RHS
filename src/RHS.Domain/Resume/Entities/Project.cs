using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class Project : Entity<ProjectId>
{
    internal Project() { } // For ORM

    private Project(ProjectId id, ResumeId resumeId, string projectTitle, string description, string? projectUrl) : base(id)
    {
        Id = id;
        ResumeId = resumeId;
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<Project> Create(ResumeId resumeId, string projectTitle, string description, string? projectUrl)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(projectTitle, nameof(projectTitle)).IsNotNullOrEmpty();
        Ensure.That(description, nameof(description)).IsNotNullOrEmpty();
        Ensure.That(projectUrl, nameof(projectUrl));
        
        return Result.Ok<Project>(new Project(ProjectId.Create(), resumeId, projectTitle, description, projectUrl));
    }
    
    public ResumeId ResumeId { get; private set; }
    public string ProjectTitle { get; private set; }
    public string Description { get; private set; }
    public string? ProjectUrl { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
}