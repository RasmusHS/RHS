using RHS.Domain.Common;

namespace RHS.Domain.Institution.ValueObjects;

public sealed class InstitutionId : ValueObject
{
    public Guid Value { get; }

    private InstitutionId(Guid value)
    {
        Value = value;
    }

    public static Result<InstitutionId> Create()
    {
        return Result.Ok<InstitutionId>(new InstitutionId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}