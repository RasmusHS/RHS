using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Domain.Skill;

public sealed class SkillSet : AggregateRoot<SkillId>
{
    internal SkillSet() { } // For ORM

    private SkillSet(SkillId id, string skillName, string type) : base(id)
    {
        Id = id;
        SkillName = skillName;
        Type = type;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    public static Result<SkillSet> Create(string skillName, string type)
    {
        Ensure.That(skillName, nameof(skillName)).IsNotNullOrEmpty();
        Ensure.That(type, nameof(type)).IsNotNullOrEmpty();
        
        return Result.Ok<SkillSet>(new SkillSet(SkillId.Create(), skillName, type));
    }
    
    public string SkillName { get; private set; }
    public string Type { get; private set; }
    
    // Navigation properties
    private readonly List<ResumeSkills> _resumes = new();
    public IReadOnlyList<ResumeSkills> Resumes => _resumes.AsReadOnly(); // many-many
}