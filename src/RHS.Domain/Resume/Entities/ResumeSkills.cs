using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;
using RHS.Domain.Skill.ValueObjects;
using RHS.Domain.Skill;

namespace RHS.Domain.Resume.Entities;

public sealed class ResumeSkills : JoinEntity<ResumeId, SkillId>
{
    internal ResumeSkills() { } // For ORM

    private ResumeSkills(ResumeId resumeId, SkillId skillId, int displayOrder) : base(resumeId, skillId)
    {
        Id1 = resumeId;
        Id2 = skillId;
        DisplayOrder = displayOrder;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<ResumeSkills> Create(ResumeId resumeId, SkillId skillId, int displayOrder)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(skillId, nameof(skillId)).IsNotNull();
        Ensure.That(displayOrder, nameof(displayOrder));
        
        return Result.Ok<ResumeSkills>(new ResumeSkills(resumeId, skillId, displayOrder));
    }
    
    public ResumeId ResumeId { get; private set; }
    public SkillId SkillId { get; private set; }
    public int DisplayOrder { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public SkillSet Skill { get; private set; }
}