using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Entities;

public sealed record Personnel : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
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