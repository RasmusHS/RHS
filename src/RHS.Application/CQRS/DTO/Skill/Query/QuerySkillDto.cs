using RHS.Application.CQRS.DTO.Skill.SubSkill.Query;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Application.CQRS.DTO.Skill.Query;

public record QuerySkillDto : DtoBase
{
    public QuerySkillDto(SkillId id, string skillName, string type, List<QuerySubSkillDto> subSkills, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        SkillName = skillName;
        Type = type;
        SubSkills = subSkills;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public QuerySkillDto() { }
    
    public SkillId Id { get; set; }
    public string SkillName { get; set; }
    public string Type { get; set; }
    public List<QuerySubSkillDto> SubSkills { get; set; }
}