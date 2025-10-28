using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;
using RHS.Domain.Skill.ValueObjects;
using RHS.Domain.Skill;

namespace RHS.Domain.Resume.Entities;

public sealed class ResumeSkills : JoinEntity<ResumeId, SkillId>
{
    internal ResumeSkills() { } // For ORM

    private ResumeSkills(ResumeId resumeId, SkillId skillId, string proficiencyLevel) : base(resumeId, skillId)
    {
        Id1 = resumeId;
        Id2 = skillId;
        ProficiencyLevel = proficiencyLevel;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<ResumeSkills> Create(ResumeId resumeId, SkillId skillId, string proficiencyLevel)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(skillId, nameof(skillId)).IsNotNull();
        Ensure.That(proficiencyLevel, nameof(proficiencyLevel)).IsNotNullOrEmpty();
        
        return Result.Ok<ResumeSkills>(new ResumeSkills(resumeId, skillId, proficiencyLevel));
    }
    
    public ResumeId ResumeId { get; private set; }
    public SkillId SkillId { get; private set; }
    public string ProficiencyLevel { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public SkillSet Skill { get; private set; }
}