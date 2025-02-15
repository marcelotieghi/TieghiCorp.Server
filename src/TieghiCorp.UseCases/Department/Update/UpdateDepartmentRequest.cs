using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Update;

public sealed record UpdateDepartmentRequest(
    int Id,
    string Name,
    int LocationId) : IRequest<Result>;