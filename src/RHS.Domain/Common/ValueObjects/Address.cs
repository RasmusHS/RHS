using EnsureThat;

namespace RHS.Domain.Common.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }

    private Address(string street, string zipcode, string city)
    {
        Street = street;
        ZipCode = zipcode;
        City = city;
    }

    public Address() { } //for ORM

    public static Result<Address> Create(string street, string zipcode, string city)
    {
        Ensure.That(street, nameof(street)).IsNotNullOrEmpty();
        Ensure.That(zipcode, nameof(zipcode)).IsNotNullOrEmpty();
        Ensure.That(city, nameof(city)).IsNotNullOrEmpty();
        
        return Result.Ok<Address>(new Address(street, zipcode, city));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return ZipCode;
        yield return City;
    }
}
