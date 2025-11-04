using Helpers;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using Assert = Xunit.Assert;

namespace RHS.Domain.Test.Resume.Entities;

[TestClass]
public class ProjectEntityTests
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenAllParametersAreValid()
    {
        // Arrange
        var resumeId = ResumeId.Create();
        var projectTitle = "Project Title";
        var description = "Project Description";
        var projectUrl = "https://example.com";
        var demoGif = new byte[] { 1, 2, 3 };
        var isFeatured = true;
        
        // Act
        var result = ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured);
        
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(resumeId.Value, result.Value.ResumeId);
        Assert.Equal(projectTitle, result.Value.ProjectTitle);
        Assert.Equal(description, result.Value.Description);
        Assert.Equal(projectUrl, result.Value.ProjectUrl);
        Assert.Equal(demoGif, result.Value.DemoGif);
        Assert.Equal(isFeatured, result.Value.IsFeatured);
    }

    [Theory]
    [InlineData("", "Project Description", "https://example.com", new byte[] { 1, 2, 3 }, true)]
    [InlineData("Project Title", "", "https://example.com", new byte[] { 1, 2, 3 }, true)]
    [InlineData("Project Title", "Project Description", "", new byte[] { 1, 2, 3 }, true)]
    public void Create_ShouldReturnFailure_WhenParametersAreEmpty(string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        // Arrange
        var resumeId = ResumeId.Create().Value;
        
        // Assert
        Assert.Throws<ArgumentException>(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
    }
    
    [Theory]
    [InlineData(null, "Project Description", "https://example.com", new byte[] { 1, 2, 3 }, true)]
    [InlineData("Project Title", null, "https://example.com", new byte[] { 1, 2, 3 }, true)]
    [InlineData("Project Title", "Project Description", null, new byte[] { 1, 2, 3 }, true)]
    [InlineData("Project Title", "Project Description", "https://example.com", null, true)]
    public void Create_ShouldReturnFailure_WhenParametersAreNull(string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        // Arrange
        var resumeId = ResumeId.Create().Value;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
    }
    
    [Fact]
    public void Update_ShouldUpdateAllFields_WhenParametersAreValid()
    {
        // Arrange
        var resumeId = ResumeId.Create().Value;
        var project = ProjectEntity.Create(resumeId, "Old Title", "Old Description", "https://oldurl.com", new byte[] { 1, 2, 3 }, false).Value;
        var newTitle = "New Title";
        var newDescription = "New Description";
        var newUrl = "https://newurl.com";
        var newDemoGif = new byte[] { 4, 5, 6 };
        var newIsFeatured = true;
        
        // Act
        project.Update(resumeId, newTitle, newDescription, newUrl, newDemoGif, newIsFeatured);
        
        // Assert
        Assert.Equal(newTitle, project.ProjectTitle);
        Assert.Equal(newDescription, project.Description);
        Assert.Equal(newUrl, project.ProjectUrl);
        Assert.Equal(newDemoGif, project.DemoGif);
        Assert.Equal(newIsFeatured, project.IsFeatured);
        Assert.True(project.LastModified > project.Created);
    }

    [Theory]
    [InlineData("", "New Description", "https://newurl.com", new byte[] { 4, 5, 6 }, true)]
    [InlineData("New Title", "", "https://newurl.com", new byte[] { 4, 5, 6 }, true)]
    [InlineData("New Title", "New Description", "", new byte[] { 4, 5, 6 }, true)]
    public void Update_ShouldThrowException_WhenParametersAreEmpty(string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        // Arrange
        var resumeId = ResumeId.Create().Value;
        var project = ProjectEntity.Create(resumeId, "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;
        
        // Assert
        Assert.Throws<ArgumentException>(() => project.Update(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
    }
    
    [Theory]
    [InlineData(null, "New Description", "https://newurl.com", new byte[] { 4, 5, 6 }, true)]
    [InlineData("New Title", null, "https://newurl.com", new byte[] { 4, 5, 6 }, true)]
    [InlineData("New Title", "New Description", null, new byte[] { 4, 5, 6 }, true)]
    [InlineData("New Title", "New Description", "https://newurl.com", null, true)]
    public void Update_ShouldThrowException_WhenParametersAreNull(string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured)
    {
        // Arrange
        var resumeId = ResumeId.Create().Value;
        var project = ProjectEntity.Create(resumeId, "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() => project.Update(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
    }
}