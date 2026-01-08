using RHS.Webapp.Models.Project.Command;

namespace RHS.Webapp.Models.Resume.Command;

public record CreateResumeModel
{
    public CreateResumeModel(string introduction, string firstName, string lastName,
        string street, string zipCode, string city,
        string email, string gitHubLink, string linkedInLink,
        byte[] photo, List<CreateProjectModel>? projects)
    {
        Introduction = introduction;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        ZipCode = zipCode;
        City = city;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;

        Projects = projects;
    }

    public CreateResumeModel(string introduction, string firstName, string lastName,
        string street, string zipCode, string city,
        string email, string gitHubLink, string linkedInLink,
        byte[] photo)
    {
        Introduction = introduction;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        ZipCode = zipCode;
        City = city;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;
    }

    public string Introduction { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string GitHubLink { get; set; }
    public string LinkedInLink { get; set; }
    public byte[] Photo { get; set; }
    public List<CreateProjectModel>? Projects { get; set; }
}
