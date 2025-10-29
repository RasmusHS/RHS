using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Language.Query;

public record QueryLanguageDto : DtoBase
{
    public QueryLanguageDto(LanguageId id, ResumeId resumeId, string name, string proficiency, DateTime created, DateTime lastModified)
    {
        Id = id;
        ResumeId = resumeId;
        Name = name;
        Proficiency = proficiency;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryLanguageDto() { }
    
    public LanguageId Id { get; set; }
    public ResumeId ResumeId { get; set; }
    public string Name { get; set; }
    public string Proficiency { get; set; }
}