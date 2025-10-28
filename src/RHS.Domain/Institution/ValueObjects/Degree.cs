using RHS.Domain.Common;

namespace RHS.Domain.Institution.ValueObjects;

public sealed class Degree : ValueObject
{
    public string Value { get; }

    private Degree(string value)
    {
        Value = value;
    }

    public static Result<Degree> Create(string value)
    {
        return Result.Ok<Degree>(new Degree(value));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}