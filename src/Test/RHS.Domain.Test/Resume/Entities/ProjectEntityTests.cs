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
    
    [Fact]
    public void Create_ShouldReturnFailure_WhenResumeIdIsNull()
    {
        // Arrange
        var projectTitle = "Project Title";
        var description = "Project Description";
        var projectUrl = "https://example.com";
        var demoGif = new byte[] { 1, 2, 3 };
        var isFeatured = true;

        // Act
        Action action = new Action(() => ProjectEntity.Create(null, projectTitle, description, projectUrl, demoGif, isFeatured));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenProjectTitleIsNullOrEmpty()
    {
        // Arrange
        var resumeId = ResumeId.Create();
        var projectTitle = "";
        var description = "Project Description";
        var projectUrl = "https://example.com";
        var demoGif = new byte[] { 1, 2, 3 };
        var isFeatured = true;
        
        // Act
        Action action1 = new Action(() => ProjectEntity.Create(resumeId, null, description, projectUrl, demoGif, isFeatured));
        Action action2 = new Action(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action1);
        Assert.Throws<ArgumentException>(action2);
    }
    
    [Fact]
    public void Create_ShouldReturnFailure_WhenDescriptionIsNullOrEmpty()
    {
        // Arrange
        var resumeId = ResumeId.Create();
        var projectTitle = "Project Title";
        var description = "";
        var projectUrl = "https://example.com";
        var demoGif = new byte[] { 1, 2, 3 };
        var isFeatured = true;
        
        // Act
        Action action1 = new Action(() => ProjectEntity.Create(resumeId, projectTitle, null, projectUrl, demoGif, isFeatured));
        Action action2 = new Action(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action1);
        Assert.Throws<ArgumentException>(action2);
    }
    
    [Fact]
    public void Create_ShouldReturnFailure_WhenProjectUrlIsNullOrEmpty()
    {
        // Arrange
        var resumeId = ResumeId.Create();
        var projectTitle = "Project Title";
        var description = "Project Description";
        var projectUrl = "";
        var demoGif = new byte[] { 1, 2, 3 };
        var isFeatured = true;
        
        // Act
        Action action1 = new Action(() => ProjectEntity.Create(resumeId, projectTitle, description, null, demoGif, isFeatured));
        Action action2 = new Action(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, demoGif, isFeatured));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action1);
        Assert.Throws<ArgumentException>(action2);
    }
    
    [Fact]
    public void Create_ShouldReturnFailure_WhenDemoGifIsNull()
    {
        // Arrange
        var resumeId = ResumeId.Create();
        var projectTitle = "Project Title";
        var description = "Project Description";
        var projectUrl = "https://example.com";
        var isFeatured = true;
        
        // Act
        Action action = new Action(() => ProjectEntity.Create(resumeId, projectTitle, description, projectUrl, null, isFeatured));
        
        // Assert
        Assert.Throws<ArgumentNullException>(action);
    }
    
    [Fact]
    public void Update_ShouldUpdateAllFields_WhenParametersAreValid()
    {
        // Arrange
        var project = ProjectEntity.Create(ResumeId.Create(), "Old Title", "Old Description", "https://oldurl.com", new byte[] { 1, 2, 3 }, false).Value;
        var newTitle = "New Title";
        var newDescription = "New Description";
        var newUrl = "https://newurl.com";
        var newDemoGif = new byte[] { 4, 5, 6 };
        var newIsFeatured = true;
        
        // Act
        project.Update(newTitle, newDescription, newUrl, newDemoGif, newIsFeatured);
        
        // Assert
        Assert.Equal(newTitle, project.ProjectTitle);
        Assert.Equal(newDescription, project.Description);
        Assert.Equal(newUrl, project.ProjectUrl);
        Assert.Equal(newDemoGif, project.DemoGif);
        Assert.Equal(newIsFeatured, project.IsFeatured);
        Assert.True(project.LastModified > project.Created);
    }
    
    [Fact]
    public void Update_ShouldThrowException_WhenProjectTitleIsNullOrEmpty()
    {
        // Arrange
        var project = ProjectEntity.Create(ResumeId.Create(), "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;

        // Assert
        Assert.Throws<ArgumentNullException>(() => project.Update(null, "Description", "https://url.com", new byte[] { 1, 2, 3 }, false));
        Assert.Throws<ArgumentException>(() => project.Update("", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false));
    }
    
    [Fact]
    public void Update_ShouldThrowException_WhenDescriptionIsNullOrEmpty()
    {
        // Arrange
        var project = ProjectEntity.Create(ResumeId.Create(), "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;

        // Assert
        Assert.Throws<ArgumentNullException>(() => project.Update("Title", null, "https://url.com", new byte[] { 1, 2, 3 }, false));
        Assert.Throws<ArgumentException>(() => project.Update("Title", "", "https://url.com", new byte[] { 1, 2, 3 }, false));
    }
    
    [Fact]
    public void Update_ShouldThrowException_WhenUrlIsNullOrEmpty()
    {
        // Arrange
        var project = ProjectEntity.Create(ResumeId.Create(), "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;

        // Assert
        Assert.Throws<ArgumentNullException>(() => project.Update("Title", "Description", null, new byte[] { 1, 2, 3 }, false));
        Assert.Throws<ArgumentException>(() => project.Update("Title", "Description", "", new byte[] { 1, 2, 3 }, false));
    }
    
    [Fact]
    public void Update_ShouldThrowException_WhenDemoGifIsNull()
    {
        // Arrange
        var project = ProjectEntity.Create(ResumeId.Create(), "Title", "Description", "https://url.com", new byte[] { 1, 2, 3 }, false).Value;

        // Assert
        Assert.Throws<ArgumentNullException>(() => project.Update("Title", "Description", "https://url.com", null, false));
    }
}