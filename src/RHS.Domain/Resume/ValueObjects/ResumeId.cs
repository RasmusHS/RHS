using RHS.Domain.Common;

namespace RHS.Domain.Resume.ValueObjects;

public sealed class ResumeId : ValueObject
{
    public Guid Value { get; }

    private ResumeId(Guid value)
    {
        Value = value;
    }

    public static Result<ResumeId> Create()
    {
        return Result.Ok<ResumeId>(new ResumeId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}