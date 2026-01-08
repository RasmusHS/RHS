namespace RHS.Webapp.Models.Project.Command;

public record DeleteProjectModel
{
    public DeleteProjectModel(Guid id, DateTime created, DateTime lastModified)
    {
        Id = id;

        Created = created;
        LastModified = lastModified;
    }

    public Guid Id { get; set; } // Project Id
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }
}
