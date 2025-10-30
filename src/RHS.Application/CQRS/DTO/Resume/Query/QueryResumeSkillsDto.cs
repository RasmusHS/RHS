using RHS.Domain.Resume.ValueObjects;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public record QueryResumeSkillsDto : DtoBase
{
    public QueryResumeSkillsDto(ResumeId resumeId, SkillId skillId, int displayOrder, DateTime created, DateTime lastModified)
    {
        ResumeId = resumeId;
        SkillId = skillId;
        
        DisplayOrder = displayOrder;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeSkillsDto() { }
    
    public ResumeId ResumeId { get; set; }
    public SkillId SkillId { get; set; }
    public int DisplayOrder { get; set; }
}