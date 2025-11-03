using Helpers;
using RHS.Domain.Common.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Domain.Test.Common.ValueObjects;

[TestClass]
public class AddressTests
{
    [Fact]
    public void Create_Valid_Address()
    {
        string street = StringRandom.GetRandomString(10);
        string zipCode = StringRandom.GetRandomString(10);
        string city = StringRandom.GetRandomString(10);
        var address = Address.Create(street, zipCode, city);
        Assert.True(address.Success);
    }

    [Theory]
    [InlineData("", "testzip", "testcity")]
    [InlineData("teststreet", "testzip", "")]
    [InlineData("teststreet", "", "testcity")]
    public void Create_Invalid_Address_EmptyParameters(string street, string zipCode, string city)
    {
        Action action = new Action(() => Address.Create(street, zipCode, city));
        Assert.Throws<ArgumentException>(action);
    }

    [Theory]
    [InlineData(null, "testzip", "testcity")]
    [InlineData("teststreet", null, "testcity")]
    [InlineData("teststreet", "testzip", null)]
    public void Create_Invalid_Address_NullParameters(string street, string zipCode, string city)
    {
        Action action = new Action(() => Address.Create(street, zipCode, city));
        Assert.Throws<ArgumentNullException>(action);
    }
}