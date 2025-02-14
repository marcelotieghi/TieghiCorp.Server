using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Entities;

public sealed record Location : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public Location() { }

    public Location(string name)
        => Name = name;
}