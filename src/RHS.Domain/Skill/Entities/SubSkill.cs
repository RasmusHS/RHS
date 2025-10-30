using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Domain.Skill.Entities;

public sealed class SubSkill : Entity<SubSkillId>
{
    internal SubSkill() { } // For ORM
    
    private SubSkill(SubSkillId id, SkillId parentSkillId, string name, bool displayed) : base(id)
    {
        Id = id;
        ParentSkillId = parentSkillId;
        
        Name = name;
        Displayed = displayed;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<SubSkill> Create(SkillId parentSkillId, string name, bool displayed)
    {
        Ensure.That(parentSkillId, nameof(parentSkillId)).IsNotNull();
        Ensure.That(name, nameof(name)).IsNotNullOrEmpty();
        
        return Result.Ok<SubSkill>(new SubSkill(SubSkillId.Create().Value, parentSkillId, name, displayed));
    }
    
    public SkillId ParentSkillId { get; private set; }
    public string Name { get; private set; }
    public bool Displayed { get; private set; }
    
    // Navigation properties
    public SkillSet ParentSkill { get; private set; }
}