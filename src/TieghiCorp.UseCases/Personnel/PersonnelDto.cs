using System.Text.Json.Serialization;
using TieghiCorp.UseCases.Department;

namespace TieghiCorp.UseCases.Personnel;

public sealed record PersonnelDto(
    int Id,
    string Firstname,
    string Lastname,
    string Email,
    string JobTitle,
    [property: JsonPropertyName("Department")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    DepartmentDto DepartmentDto = default!);