namespace RHS.Webapp.Models.Project.Command;

public record UpdateProjectModel
{
    public UpdateProjectModel(Guid id, Guid resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured, DateTime created, DateTime lastModified)
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

    public Guid Id { get; set; } // Project Id
    public Guid ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public byte[] DemoGif { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }
}
