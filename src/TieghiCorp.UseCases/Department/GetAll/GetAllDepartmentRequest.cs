using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.GetAll;

public sealed record GetAllDepartmentRequest(
    int Page = 1,
    int PageSize = 25,
    string SearchTerm = "",
    string SortField = "id",
    string SortDirection = "asc") : IRequest<PagedResult<IEnumerable<DepartmentDto>>>;