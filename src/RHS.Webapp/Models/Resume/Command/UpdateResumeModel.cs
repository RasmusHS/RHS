namespace RHS.Webapp.Models.Resume.Command;

public record UpdateResumeModel
{
    public UpdateResumeModel(Guid id, string introduction, string firstName, string lastName,
        string street, string zipCode, string city,
        string email, string gitHubLink, string linkedInLink, byte[] photo, DateTime created, DateTime lastModified)
    {
        Id = id;

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

        Created = created;
        LastModified = lastModified;
    }

    public Guid Id { get; set; } // Resume Id
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
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }
}
