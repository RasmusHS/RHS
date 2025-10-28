using RHS.Domain.Institution.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Education.Query;

public class QueryEducationDto : DtoBase
{
    public QueryEducationDto(ResumeId resumeId, Degree degree, DateTime startDate, DateTime? endDate, DateTime created, DateTime lastModified)
    {
        ResumeId = resumeId;
        Degree = degree;
        
        StartDate = startDate;
        EndDate = endDate;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryEducationDto() { }
    
    public ResumeId ResumeId { get; set; }
    public Degree Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}