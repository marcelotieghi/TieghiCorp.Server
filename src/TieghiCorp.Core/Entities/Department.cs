using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Entities;

public sealed record Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location? Location { get; set; }

    // Constructor required by Entity Framework
    public Department() { }

    public Department(
        string name,
        int locationId)
    {
        Name = name;
        LocationId = locationId;
    }
}