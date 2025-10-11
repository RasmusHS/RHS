using RHS.Domain.Common;
using RHS.Domain.Institution;

namespace RHS.Domain.Entities;

public class ResumeEdu : JoinEntity
{
    internal ResumeEdu() { } // For ORM

    public ResumeEdu(int resumeId, int eduId)
    {
        ResumeId = resumeId;
        EducationId = eduId;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public int ResumeId { get; private set; }
    public int EducationId { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public Institution.Institution Institution { get; private set; }
}