using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class Language : Entity
{
    internal Language() { } // For ORM

    public Language(/*int languageId,*/ int resumeId, string name, string proficiency)
    {
        //Id = languageId;
        ResumeId = resumeId;
        Name = name;
        Proficiency = proficiency;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    
    public int ResumeId { get; private set; }
    public string Name { get; private set; }
    public string Proficiency { get; private set; }
}