using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.WorkExperience.Query;

public class QueryWorkExperienceDto : DtoBase
{
    public QueryWorkExperienceDto(WorkExpId id, ResumeId resumeId, string title, string company, Address location, DateTime startDate, DateTime? endDate, string description, DateTime created, DateTime lastModified)
    {
        Id = id;
        ResumeId = resumeId;
        
        Title = title;
        Company = company;
        Location = location;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryWorkExperienceDto() { }
    
    public WorkExpId Id { get; set; }
    public ResumeId ResumeId { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public Address Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}