using Helpers;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Domain.Test.Resume;

[TestClass]
public class ResumeEntityTests
{
    [Fact]
    public void Create_Valid_Resume()
    {
        // Arrange
        var introduction = "Introduction text";
        var fullName = FullName.Create("John", "Doe").Value;
        var address = Address.Create("123 Street", "12345", "City").Value;
        var email = Email.Create("test@example.com").Value;
        var gitHubLink = "https://github.com/johndoe";
        var linkedInLink = "https://linkedin.com/in/johndoe";
        var photo = new byte[] { 1, 2, 3 };
        
        // Act
        var result = ResumeEntity.Create(introduction, fullName, address, email, gitHubLink, linkedInLink, photo);
        
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(introduction, result.Value.Introduction);
        Assert.Equal(fullName, result.Value.FullName);
        Assert.Equal(address, result.Value.Address);
        Assert.Equal(email, result.Value.Email);
        Assert.Equal(gitHubLink, result.Value.GitHubLink);
        Assert.Equal(linkedInLink, result.Value.LinkedInLink);
        Assert.Equal(photo, result.Value.Photo);
    }

    [Theory]
    [InlineData("", "John", "Doe", "123 Street", "12345", "City", "test@example.com", "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 })]
    [InlineData("Introduction text", "John", "Doe", "123 Street", "12345", "City", "test@example.com", "", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 })]
    [InlineData("Introduction text", "John", "Doe", "123 Street", "12345", "City", "test@example.com", "https://github.com/johndoe", "", new byte[] { 1, 2, 3 })]
    public void Create_ShouldReturnFailure_WhenParametersAreEmpty(string introduction, string firstName, string lastName, string street, string zipcode, string city, string email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        // Arrange
        var fullName = FullName.Create(firstName, lastName).Value;
        var address = Address.Create(street, zipcode, city).Value;
        var emailVar = Email.Create(email).Value;
        
        // Act
        Action action = new Action(() => ResumeEntity.Create(introduction, fullName, address, emailVar, gitHubLink, linkedInLink, photo));
        
        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
    [Theory]
    [InlineData(null, "John", "Doe", "123 Street", "12345", "City", "test@example.com", "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 })]
    [InlineData("Introduction text", "John", "Doe", "123 Street", "12345", "City", "test@example.com", null, "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 })]
    [InlineData("Introduction text", "John", "Doe", "123 Street", "12345", "City", "test@example.com", "https://github.com/johndoe", null, new byte[] { 1, 2, 3 })]
    [InlineData("Introduction text", "John", "Doe", "123 Street", "12345", "City", "test@example.com", "https://github.com/johndoe", "https://linkedin.com/in/johndoe", null)]
    public void Create_ShouldReturnFailure_WhenParametersAreNull(string introduction, string firstName, string lastName, string street, string zipcode, string city, string email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        // Arrange
        var fullName = FullName.Create(firstName, lastName).Value;
        var address = Address.Create(street, zipcode, city).Value;
        var emailVar = Email.Create(email).Value;
        
        // Act
        Action action = new Action(() => ResumeEntity.Create(introduction, fullName, address, emailVar, gitHubLink, linkedInLink, photo));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action);
        Assert.Throws<ArgumentNullException>(() => ResumeEntity.Create(introduction, null, address, emailVar, gitHubLink, linkedInLink, photo));
        Assert.Throws<ArgumentNullException>(() => ResumeEntity.Create(introduction, fullName, null, emailVar, gitHubLink, linkedInLink, photo));
        Assert.Throws<ArgumentNullException>(() => ResumeEntity.Create(introduction, fullName, address, null, gitHubLink, linkedInLink, photo));
    }
    
    [Fact]
    public void AddProject_ShouldAddProject_WhenProjectIsValid()
    {
        // Arrange
        var resume = ResumeEntity.Create("Introduction", FullName.Create("John", "Doe").Value, Address.Create("123 Street", "12345", "City").Value, Email.Create("test@example.com").Value, "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 }).Value;
        var project = ProjectEntity.Create(resume.Id, "Project Title", "Description", "https://projecturl.com", new byte[] { 4, 5, 6 }, true).Value;
        
        // Act
        resume.AddProject(project);
        
        // Assert
        Assert.Contains(project, resume.Projects);
    }
    
    [Fact]
    public void AddProject_ShouldThrowException_WhenProjectIsNull()
    {
        // Arrange
        var resume = ResumeEntity.Create("Introduction", FullName.Create("John", "Doe").Value, Address.Create("123 Street", "12345", "City").Value, Email.Create("test@example.com").Value, "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 }).Value;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => resume.AddProject(null));

    }
    
    [Fact]
    public void AddRangeOf_Projects_To_Resume()
    {
        // Arrange
        var resume = ResumeEntity.Create("Introduction", FullName.Create("John", "Doe").Value, Address.Create("123 Street", "12345", "City").Value, Email.Create("test@example.com").Value, "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 }).Value;
        var project1 = ProjectEntity.Create(resume.Id, "Project Title1", "Description1", "https://projecturl1.com", new byte[] { 4, 5, 6 }, true).Value;
        var project2 = ProjectEntity.Create(resume.Id, "Project Title2", "Description2", "https://projecturl2.com", new byte[] { 4, 5, 6 }, true).Value;
        var projects = new List<ProjectEntity> { project1, project2 };
        
        // Act
        resume.AddRangeProjects(projects);
        
        // Assert
        Assert.Contains(project1, resume.Projects);
        Assert.Contains(project2, resume.Projects);
    }
    
    [Fact]
    public void AddRangeOf_Null_Projects_To_Resume_Throws()
    {
        // Arrange
        var resume = ResumeEntity.Create("Introduction", FullName.Create("John", "Doe").Value, Address.Create("123 Street", "12345", "City").Value, Email.Create("test@example.com").Value, "https://github.com/johndoe", "https://linkedin.com/in/johndoe", new byte[] { 1, 2, 3 }).Value;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => resume.AddRangeProjects(null));
    }
    
    // Update Tests
    [Fact]
    public void Update_ShouldUpdateAllFields_WhenParametersAreValid()
    {
        // Arrange
        var resume = ResumeEntity.Create("Old Introduction", FullName.Create("Jane", "Doe").Value, Address.Create("456 Avenue", "67890", "OldCity").Value, Email.Create("old@example.com").Value, "https://github.com/janedoe", "https://linkedin.com/in/janedoe", new byte[] { 4, 5, 6 }).Value;
        var newIntroduction = "New Introduction";
        var newFirstName = "John";
        var newLastName = "Smith";
        var newStreet = "789 Boulevard";
        var newZipCode = "12345";
        var newCity = "NewCity";
        var newEmail = "new@example.com";
        var newGitHubLink = "https://github.com/johnsmith";
        var newLinkedInLink = "https://linkedin.com/in/johnsmith";
        var newPhoto = new byte[] { 7, 8, 9 };
        
        // Act
        resume.Update(newIntroduction, newFirstName, newLastName, newStreet, newZipCode, newCity, newEmail, newGitHubLink, newLinkedInLink, newPhoto);
        
        // Assert
        Assert.Equal(newIntroduction, resume.Introduction);
        Assert.Equal(newFirstName, resume.FullName.FirstName);
        Assert.Equal(newLastName, resume.FullName.LastName);
        Assert.Equal(newStreet, resume.Address.Street);
        Assert.Equal(newZipCode, resume.Address.ZipCode);
        Assert.Equal(newCity, resume.Address.City);
        Assert.Equal(newEmail, resume.Email.Value);
        Assert.Equal(newGitHubLink, resume.GitHubLink);
        Assert.Equal(newLinkedInLink, resume.LinkedInLink);
        Assert.Equal(newPhoto, resume.Photo);
        Assert.True(resume.LastModified > resume.Created);
    }
    
    [Theory]
    [InlineData("", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "", new byte[] { 7, 8, 9 })]
    public void Update_ShouldThrowException_WhenParametersAreEmpty(string intro, string firstName, string lastName, string street, string zipCode, string city, string email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        // Arrange
        var resume = ResumeEntity.Create("Old Introduction", FullName.Create("Jane", "Doe").Value, Address.Create("456 Avenue", "67890", "OldCity").Value, Email.Create("old@example.com").Value, "https://github.com/janedoe", "https://linkedin.com/in/janedoe", new byte[] { 4, 5, 6 }).Value;
        
        // Assert
        Assert.Throws<ArgumentException>(() => resume.Update(intro, firstName, lastName, street, zipCode, city, email, gitHubLink, linkedInLink, photo));
    }
    
    [Theory]
    [InlineData(null, "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", null, "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", null, "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", null, "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", null, "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", null, "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", null, "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", null, "https://linkedin.com/in/johnsmith", new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", null, new byte[] { 7, 8, 9 })]
    [InlineData("New Introduction", "John", "Smith", "789 Boulevard", "12345", "NewCity", "new@example.com", "https://github.com/johnsmith", "https://linkedin.com/in/johnsmith", null)]
    public void Update_ShouldThrowException_WhenParametersAreNull(string intro, string firstName, string lastName, string street, string zipCode, string city, string email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        // Arrange
        var resume = ResumeEntity.Create("Old Introduction", FullName.Create("Jane", "Doe").Value, Address.Create("456 Avenue", "67890", "OldCity").Value, Email.Create("old@example.com").Value, "https://github.com/janedoe", "https://linkedin.com/in/janedoe", new byte[] { 4, 5, 6 }).Value;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => resume.Update(intro, firstName, lastName, street, zipCode, city, email, gitHubLink, linkedInLink, photo));
    }
}