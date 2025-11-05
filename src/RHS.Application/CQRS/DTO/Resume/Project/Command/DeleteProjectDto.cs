using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Project.Command;

public record DeleteProjectDto : DtoBase
{
    public DeleteProjectDto(ProjectId id, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public DeleteProjectDto() { }
    
    public ProjectId Id { get; set; }
}