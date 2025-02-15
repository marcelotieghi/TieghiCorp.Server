using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Delete;

public sealed record DeleteDepartmentRequest(int Id) : IRequest<Result>;