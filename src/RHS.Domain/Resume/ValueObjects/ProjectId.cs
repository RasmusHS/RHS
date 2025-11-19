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
    }

    public static Result<ProjectId> GetExisting(Guid value)
    {
        return Result.Ok<ProjectId>(new ProjectId(value));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}