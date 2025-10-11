using RHS.Domain.Common;

namespace RHS.Domain.Resume.ValueObjects;

public sealed class WorkExpId : ValueObject
{
    public Guid Value { get; }

    private WorkExpId(Guid value)
    {
        Value = value;
    }

    public static Result<WorkExpId> Create()
    {
        return Result.Ok<WorkExpId>(new WorkExpId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}