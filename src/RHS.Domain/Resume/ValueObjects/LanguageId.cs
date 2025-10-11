using RHS.Domain.Common;

namespace RHS.Domain.Resume.ValueObjects;

public sealed class LanguageId : ValueObject
{
    public Guid Value { get; }

    private LanguageId(Guid value)
    {
        Value = value;
    }

    public static Result<LanguageId> Create()
    {
        return Result.Ok<LanguageId>(new LanguageId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}