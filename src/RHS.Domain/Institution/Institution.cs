using RHS.Domain.Common;
using RHS.Domain.Entities;
using RHS.Domain.Institution.ValueObjects;

namespace RHS.Domain.Institution;

public sealed class Institution : AggregateRoot<InstitutionId>
{
    internal Institution() { } // For ORM

    public Institution(DateTime startDate, DateTime? endDate, string institutionName, string degree)
    {
        StartDate = startDate;
        EndDate = endDate;
        InstitutionName = institutionName;
        Degree = degree;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public DateTime StartDate { get; private set; } // TODO: Move to ResumeEdu
    public DateTime? EndDate { get; private set; } // TODO: Move to ResumeEdu
    public string InstitutionName { get; private set; } // TODO: Add new properties like location 
    public string Degree { get; private set; } // TODO: Move to ResumeEdu
    
    // Navigation properties
    public List<ResumeEdu> Resumes { get; private set; } // many-many
}