namespace RHS.Webapp.Models.Project.Command;

public record CreateProjectModel
{
    public CreateProjectModel(Guid? resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        ResumeId = resumeId;

        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
    }

    public Guid? ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public byte[] DemoGif { get; set; }
    public bool IsFeatured { get; set; }
}
