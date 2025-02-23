using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Entities;

public sealed record Location : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Constructor required by Entity Framework
    public Location() { }

    public Location(string name) => Name = name;
}