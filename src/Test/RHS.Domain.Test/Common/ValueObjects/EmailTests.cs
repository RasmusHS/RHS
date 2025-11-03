using RHS.Domain.Common.ValueObjects;
using Helpers;
using Assert = Xunit.Assert;

namespace RHS.Domain.Test.Common.ValueObjects;

[TestClass]
public class EmailTests
{
    [Fact]
    public void Create_Valid_Email()
    {
        var email = Email.Create("valid@email.com");
        Assert.True(email.Success);
    }

    [Fact]
    public void Create_Invalid_Email()
    {
        var email = Email.Create("invalidemail"); // invalid, does not follow pattern
        Assert.True(email.Failure);
    }

    [Fact]
    public void Create_Invalid_Email_given_empty()
    {
        var email = Email.Create(""); // invalid empty  
        Assert.True(email.Failure);
    }

    [Fact]
    public void Create_Invalid_Email_given_null()
    {
        var email = Email.Create(null); // invalid null
        Assert.True(email.Failure);
    }
    
    [Fact]
    public void Create_Invalid_Email_Too_Long()
    {
        var email = Email.Create(StringRandom.GetRandomString(101));
        Assert.True(email.Failure);
    }
    
    [Fact]
    public void Create_Two_Emails_Expect_Equal()
    {
        string email = "test@test.dk";
        Email email1 = Email.Create(email).Value;
        Email email2 = Email.Create(email).Value;
        Assert.Equal(email1, email2);
    }
    
    [Fact]
    public void Create_Two_Emails_Expect_Different()
    {
        string email1 = "test1@test.dk";
        string email2 = "test2@test.dk";
        var testEmail1 = Email.Create(email1).Value;
        var testEmail2 = Email.Create(email2).Value;
        Assert.NotEqual(testEmail1, testEmail2);
    }
}