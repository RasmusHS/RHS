using Helpers;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Domain.Test.Resume.ValueObjects;

[TestClass]
public class FullNameTests
{
    [Fact]
    public void Create_Valid_FullName()
    {
        // Arrange
        var firstName = StringRandom.GetRandomAlphabeticString(10);
        var lastName = StringRandom.GetRandomAlphabeticString(10);

        // Act
        var fullName = FullName.Create(firstName, lastName);

        // Assert
        Assert.True(fullName.Success);
    }

    [Fact]
    public void Create_Invalid_FullName_EmptyFirstName()
    {
        // Arrange
        var firstName = "";
        var lastName = StringRandom.GetRandomAlphabeticString(10);

        // Act
        Action action = new Action(() => FullName.Create(firstName, lastName));

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_Invalid_FullName_EmptyLastName()
    {
        // Arrange
        var firstName = StringRandom.GetRandomAlphabeticString(10);
        var lastName = "";

        // Act
        Action action = new Action(() => FullName.Create(firstName, lastName));

        // Assert
        Assert.Throws<ArgumentException>(action); 
    }

    [Fact]
    public void Create_Invalid_FullName_EmptyParameters()
    {
        // Arrange
        var firstName = "";
        var lastName = "";

        // Act
        Action action = new Action(() => FullName.Create(firstName, lastName));

        // Assert
        Assert.Throws<ArgumentException>(action); 
    }

    [Fact]
    public void Create_Invalid_FullName_NullFirstName()
    {
        // Arrange
        var firstName = "";
        var lastName = StringRandom.GetRandomAlphabeticString(10);

        // Act
        Action action = new Action(() => FullName.Create(null, lastName));

        // Assert
        Assert.Throws<ArgumentNullException>(action); 
    }
    
    [Fact]
    public void Create_Invalid_FullName_NullLastName()
    {
        // Arrange
        var firstName = StringRandom.GetRandomAlphabeticString(10);
        var lastName = "";

        // Act
        Action action = new Action(() => FullName.Create(firstName, null));

        // Assert
        Assert.Throws<ArgumentNullException>(action); 
    }

    [Fact]
    public void Create_Invalid_FullName_NullParameters()
    {
        // Arrange
        var firstName = "";
        var lastName = "";

        // Act
        Action action = new Action(() => FullName.Create(null, null));

        // Assert
        Assert.Throws<ArgumentNullException>(action); 
    }
    
    // [Fact]
    // public void Create_Invalid_FullName_FirstNameWithNumbers()
    // {
    //     // Arrange
    //     var firstName = "John123";
    //     var lastName = "Doe";
    //
    //     // Act
    //     Action action = new Action(() => FullName.Create(firstName, lastName));
    //
    //     // Assert
    //     Assert.Throws<ArgumentException>(action); 
    // }
}