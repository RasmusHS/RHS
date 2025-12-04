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
        
        //var resumeId = ResumeId.Create().Value;
        var resumeId = ResumeId.GetExisting(Guid.Parse("5ec199f0-4c26-4d20-bbdd-39ac844237b8")).Value;
        modelBuilder.Entity<ResumeEntity>().HasData(new
        {
            Id = resumeId,
            Introduction = "TestIntro",
            full_name_first_name = FullName.Create("John", "Doe").Value.FirstName,
            full_name_last_name = FullName.Create("John", "Doe").Value.LastName,
            address_street = "TestStreet 1",
            address_zip_code = "9000",
            address_city = "Aalborg",
            Email = Email.Create("TestMail@TestDomain.dk").Value,
            //Email = "TestMail@TestDomain.dk",
            GitHubLink = "https://github.com/RasmusHS",
            LinkedInLink = "https://www.linkedin.com/in/rasmus-h%C3%B8y-s-40079513a/",
            Photo = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "IMG_0633.jpg")),
            Created = DateTime.Parse("12/4/2025 4:56:15 PM"),
            LastModified = DateTime.Parse("12/4/2025 4:56:15 PM")
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
        
        //var projectId1 = ProjectId.Create().Value;
        //var projectId2 = ProjectId.Create().Value;
        var projectId1 = ProjectId.GetExisting(Guid.Parse("87e2a1b5-9f38-4a99-b682-bfa485f99d90")).Value;
        var projectId2 = ProjectId.GetExisting(Guid.Parse("62f5d72c-2aab-45b3-9faf-d21f7b8c78d7")).Value;

        modelBuilder.Entity<ProjectEntity>().HasData(new
        {
            Id = projectId1,
            ResumeId = resumeId,
            ProjectTitle = "TestProjectTitle1",
            Description = "TestProjectDescription1",
            ProjectUrl = "TestProjectUrl1.dk",
            DemoGif = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "10f.gif")),
            IsFeatured = true,
            Created = DateTime.Parse("12/4/2025 4:56:15 PM"),
            LastModified = DateTime.Parse("12/4/2025 4:56:15 PM")
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
            Created = DateTime.Parse("12/4/2025 4:56:15 PM"),
            LastModified = DateTime.Parse("12/4/2025 4:56:15 PM")
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