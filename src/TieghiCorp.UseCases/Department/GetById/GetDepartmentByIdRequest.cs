using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.GetById;

public sealed record GetDepartmentByIdRequest(int Id) : IRequest<Result<DepartmentDto>>;