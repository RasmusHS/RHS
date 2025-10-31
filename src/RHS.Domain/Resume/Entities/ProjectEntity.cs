using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class ProjectEntity : Entity<ProjectId>
{
    internal ProjectEntity() { } // For ORM

    private ProjectEntity(ProjectId id, ResumeId resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured) : base(id)
    {
        Id = id;
        ResumeId = resumeId;
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<ProjectEntity> Create(ResumeId resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(projectTitle, nameof(projectTitle)).IsNotNullOrEmpty();
        Ensure.That(description, nameof(description)).IsNotNullOrEmpty();
        Ensure.That(projectUrl, nameof(projectUrl)).IsNotNullOrEmpty();
        Ensure.That(demoGif, nameof(demoGif)).IsNotNull();
        
        return Result.Ok<ProjectEntity>(new ProjectEntity(ProjectId.Create(), resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
    }
    
    public ResumeId ResumeId { get; private set; }
    public string ProjectTitle { get; private set; }
    public string Description { get; private set; }
    public string ProjectUrl { get; private set; }
    public byte[] DemoGif { get; private set; }
    public bool IsFeatured { get; private set; }
    
    // navigation properties
    public ResumeEntity ResumeEntity { get; private set; }
}