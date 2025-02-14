using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Entities;

public sealed record Personnel : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public Personnel() { }

    public Personnel(
        string firstName,
        string lastName,
        string email,
        string jobTitle,
        int departmentId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        JobTitle = jobTitle;
        DepartmentId = departmentId;
    }
}