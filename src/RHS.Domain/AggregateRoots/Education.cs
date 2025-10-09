using RHS.Domain.Common;
using RHS.Domain.Entities;

namespace RHS.Domain.AggregateRoots;

public class Education : AggregateRoot
{
    internal Education() { } // For ORM

    public Education(DateTime startDate, DateTime? endDate, string institutionName, string degree)
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