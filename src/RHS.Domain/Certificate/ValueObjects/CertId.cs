using RHS.Domain.Common;

namespace RHS.Domain.Certificate.ValueObjects;

public sealed class CertId : ValueObject
{
    public Guid Value { get; }

    private CertId(Guid value)
    {
        Value = value;
    }

    public static Result<CertId> Create()
    {
        return Result.Ok<CertId>(new CertId(Guid.NewGuid()));
        //return new(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}