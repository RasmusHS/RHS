using RHS.Domain.Common;

namespace RHS.Domain.Skill.ValueObjects;

public sealed class SubSkillId : ValueObject
{
    public Guid Value { get; }
    
    private SubSkillId(Guid value)
    {
        Value = value;
    }
    
    public static Result<SubSkillId> Create()
    {
        return Result.Ok<SubSkillId>(new SubSkillId(Guid.NewGuid()));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}