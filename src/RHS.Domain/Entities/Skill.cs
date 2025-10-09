using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class Skill : Entity
{
    internal Skill() { } // For ORM

    public Skill(string skillName, string proficiencyLevel)
    {
        SkillName = skillName;
        ProficiencyLevel = proficiencyLevel;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public string SkillName { get; private set; }
    public string ProficiencyLevel { get; private set; }
}