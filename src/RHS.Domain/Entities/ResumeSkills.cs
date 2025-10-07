using RHS.Domain.AggregateRoots;
using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class ResumeSkills : JoinEntity
{
    internal ResumeSkills()
    {
        
    }

    public ResumeSkills(int resumeId, int skillId)
    {
        ResumeId = resumeId;
        SkillId = skillId;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public int ResumeId { get; private set; }
    public int SkillId { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public Skill Skill { get; private set; }
}