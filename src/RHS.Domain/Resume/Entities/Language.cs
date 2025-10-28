using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class Language : Entity<LanguageId>
{
    internal Language() { } // For ORM

    private Language(LanguageId id, ResumeId resumeId, string name, string proficiency) : base(id)
    {
        Id = id;
        ResumeId = resumeId;
        Name = name;
        Proficiency = proficiency;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<Language> Create(ResumeId resumeId, string name, string proficiency)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(name, nameof(name)).IsNotNullOrEmpty();
        Ensure.That(proficiency, nameof(proficiency)).IsNotNullOrEmpty();
        
        return Result.Ok<Language>(new Language(LanguageId.Create(), resumeId, name, proficiency));
    }
    
    public ResumeId ResumeId { get; private set; }
    public string Name { get; private set; }
    public string Proficiency { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
}