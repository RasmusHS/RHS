using EnsureThat;
using RHS.Domain.Common;

namespace RHS.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; set; }
    public string Number { get; set; }
    
    private PhoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }
    
    public PhoneNumber() { } //for ORM
    
    public static Result<PhoneNumber> Create(string countryCode, string number)
    {
        Ensure.That(countryCode, nameof(countryCode)).IsNotNullOrEmpty();
        Ensure.That(number, nameof(number)).IsNotNullOrEmpty();
        return Result.Ok<PhoneNumber>(new PhoneNumber(countryCode, number));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }
}