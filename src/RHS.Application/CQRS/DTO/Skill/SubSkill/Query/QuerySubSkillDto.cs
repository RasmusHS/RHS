using RHS.Domain.Skill.ValueObjects;

namespace RHS.Application.CQRS.DTO.Skill.SubSkill.Query;

public record QuerySubSkillDto : DtoBase
{
    public QuerySubSkillDto(SubSkillId id, SkillId parentSkillId, string name,  bool displayed, DateTime created, DateTime lastModified)
    {
        Id = id;
        ParentSkillId = parentSkillId;
        
        Name = name;
        Displayed = displayed;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public QuerySubSkillDto() { }
    
    public SubSkillId Id { get; set; }
    public SkillId ParentSkillId { get; set; }
    public string Name { get; set; }
    public bool Displayed { get; set; }
}