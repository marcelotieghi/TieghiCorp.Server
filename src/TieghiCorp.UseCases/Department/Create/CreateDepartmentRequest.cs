using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Create;

public sealed record CreateDepartmentRequest(string Name, int LocationId) : IRequest<Result>;