using System.Text.Json.Serialization;
using TieghiCorp.UseCases.Location;

namespace TieghiCorp.UseCases.Department;

public sealed record DepartmentDto(
    int Id,
    string Name,
    [property: JsonPropertyName("Location")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    LocationDto LocationDto = default!);