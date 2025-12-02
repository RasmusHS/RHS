using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<ResumeEntity> Resumes { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    
    public void SaveChanges(CancellationToken cancellationToken = default)
    {
        base.SaveChanges();
        //base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        var resumeId = ResumeId.Create().Value;
        modelBuilder.Entity<ResumeEntity>().HasData(new
        {
            Id = resumeId,
            Introduction = "TestIntro",
            FullName_FirstName = "John",
            FullName_LastName = "Doe",
            Address_Street = "TestStreet 1",
            Address_ZipCode = "9000",
            Address_City = "Aalborg",
            Email = Email.Create("TestMail@TestDomain.dk").Value,
            GitHubLink = "https://github.com/RasmusHS",
            LinkedInLink = "https://www.linkedin.com/in/rasmus-h%C3%B8y-s-40079513a/",
            Photo = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "IMG_0633.jpg")),
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        });
        
        // modelBuilder.Entity<ResumeEntity>()
        //     .HasData(
        //         resumeId, 
        //         "TestIntro", 
        //         FullName.Create("John", "Doe").Value, 
        //         Address.Create("TestStreet 1", "9000", "Aalborg").Value, 
        //         Email.Create("TestMail@TestDomain.dk").Value, 
        //         "https://github.com/RasmusHS", 
        //         "https://www.linkedin.com/in/rasmus-h%C3%B8y-s-40079513a/", 
        //         ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "IMG_0633.jpg")),
        //         DateTime.UtcNow,
        //         DateTime.UtcNow);
        //
        // modelBuilder.Entity<ResumeEntity>()
        //     .HasData(ResumeEntity.Create(
        //         "TestIntro", 
        //         FullName.Create("John", "Doe").Value, 
        //         Address.Create("TestStreet 1", "9000", "Aalborg").Value, 
        //         Email.Create("TestMail@TestDomain.dk").Value, 
        //         "https://github.com/RasmusHS", 
        //         "https://www.linkedin.com/in/rasmus-h%C3%B8y-s-40079513a/", 
        //         ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "IMG_0633.jpg"))));
        
        var projectId1 = ProjectId.Create().Value;
        var projectId2 = ProjectId.Create().Value;

        modelBuilder.Entity<ProjectEntity>().HasData(new
        {
            Id = projectId1,
            ResumeId = resumeId,
            ProjectTitle = "TestProjectTitle1",
            Description = "TestProjectDescription1",
            ProjectUrl = "TestProjectUrl1.dk",
            DemoGif = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "10f.gif")),
            IsFeatured = true,
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        });
        
        modelBuilder.Entity<ProjectEntity>().HasData(new
        {
            Id = projectId2,
            ResumeId = resumeId,
            ProjectTitle = "TestProjectTitle2",
            Description = "TestProjectDescription2",
            ProjectUrl = "TestProjectUrl2.dk",
            DemoGif = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "10f.gif")),
            IsFeatured = true,
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        });
        
        // modelBuilder.Entity<ProjectEntity>()
        //     .HasData(
        //         projectId1,
        //         resumeId,
        //         "TestProjectTitle1",
        //         "TestProjectDescription1",
        //         "TestProjectUrl1.dk",
        //         ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "10f.gif")),
        //         true,
        //         DateTime.UtcNow,
        //         DateTime.UtcNow);
        
        // modelBuilder.Entity<ProjectEntity>()
        //     .HasData(
        //         projectId2,
        //         resumeId,
        //         "TestProjectTitle2",
        //         "TestProjectDescription2",
        //         "TestProjectUrl2.dk",
        //         ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "10f.gif")),
        //         true,
        //         DateTime.UtcNow,
        //         DateTime.UtcNow);
    }

    private byte[] ImageToByteArray(string imagePath)
    {
        return File.ReadAllBytes(imagePath);
    }
}