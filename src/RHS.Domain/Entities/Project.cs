using RHS.Domain.AggregateRoots;
using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class Project : Entity
{
    internal Project()
    {
        
    }

    public Project(/*int projectId,*/ int resumeId, string projectTitle, string description, string? projectUrl)
    {
        //Id = projectId;
        ResumeId = resumeId;
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public int ResumeId { get; private set; }
    public string ProjectTitle { get; private set; }
    public string Description { get; private set; }
    public string? ProjectUrl { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
}