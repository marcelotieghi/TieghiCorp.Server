using TieghiCorp.UseCases.Location;

namespace TieghiCorp.UseCases.Department;

public sealed record DepartmentDto(
    int Id,
    string Name,
    LocationDto LocationDto);