using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Domain.Certificate;
using RHS.Domain.Institution;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Skill;
using RHS.Domain.Skill.Entities;

namespace RHS.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<ResumeSkills> ResumeSkills { get; set; }
    public DbSet<ResumeCerts> ResumeCerts { get; set; }
    public DbSet<InstitutionEntity> Institutions { get; set; }
    public DbSet<SkillSet> Skills { get; set; }
    public DbSet<SubSkill> SubSkills { get; set; }
    public DbSet<Cert> Certs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //base.OnModelCreating(modelBuilder);
    }
}