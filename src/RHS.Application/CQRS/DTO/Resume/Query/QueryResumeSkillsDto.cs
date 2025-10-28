using RHS.Domain.Resume.ValueObjects;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public class QueryResumeSkillsDto : DtoBase
{
    public QueryResumeSkillsDto(ResumeId resumeId, SkillId skillId, string proficiencyLevel, DateTime created, DateTime lastModified)
    {
        ResumeId = resumeId;
        SkillId = skillId;
        
        ProficiencyLevel = proficiencyLevel;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeSkillsDto() { }
    
    public ResumeId ResumeId { get; set; }
    public SkillId SkillId { get; set; }
    public string ProficiencyLevel { get; set; }
}