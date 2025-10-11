using RHS.Domain.Common;

namespace RHS.Domain.Resume.ValueObjects;

public sealed class ProjectId : ValueObject
{
    public Guid Value { get; }

    private ProjectId(Guid value)
    {
        Value = value;
    }

    public static Result<ProjectId> Create()
    {
        return Result.Ok<ProjectId>(new ProjectId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}