using RHS.Domain.Common;

namespace RHS.Domain.Skill.ValueObjects;

public sealed class SkillId : ValueObject
{
    public Guid Value { get; }

    private SkillId(Guid value)
    {
        Value = value;
    }

    public static Result<SkillId> Create()
    {
        return Result.Ok<SkillId>(new SkillId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}