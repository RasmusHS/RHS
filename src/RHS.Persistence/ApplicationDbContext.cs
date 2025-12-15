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
        
        // Only apply seed data if not in testing environment
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment != "Testing")
        {
            var resumeId = ResumeId.GetExisting(Guid.Parse("5ec199f0-4c26-4d20-bbdd-39ac844237b8")).Value;
            modelBuilder.Entity<ResumeEntity>().HasData(new
            {
                Id = resumeId,
                Introduction = "TestIntro",
                Email = Email.Create("TestMail@TestDomain.dk").Value,
                GitHubLink = "https://github.com/RasmusHS",
                LinkedInLink = "https://www.linkedin.com/in/rasmus-h%C3%B8y-s-40079513a/",
                Photo = ImageToByteArray(Path.Combine(AppContext.BaseDirectory, "SeedFiles", "IMG_0633.jpg")),
                Created = DateTime.Parse("12/4/2025 4:56:15 PM"),
                LastModified = DateTime.Parse("12/4/2025 4:56:15 PM")
            });

            modelBuilder.Entity<ResumeEntity>()
                .OwnsOne(f => f.FullName)
                .HasData(new
                {
                    ResumeEntityId = resumeId,
                    FirstName = "John",
                    LastName = "Doe"
                });
            modelBuilder.Entity<ResumeEntity>()
                .OwnsOne(a => a.Address)
                .HasData(new
                {
                    ResumeEntityId = resumeId,
                    Street = "TestStreet 1",
                    ZipCode = "9000",
                    City = "Aalborg"
                });

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
        }
    }

    private byte[] ImageToByteArray(string imagePath)
    {
        return File.ReadAllBytes(imagePath);
    }
}