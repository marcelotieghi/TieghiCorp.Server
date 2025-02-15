using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.GetAll;

public sealed record GetAllDepartmentRequest(
    int Page,
    int PageSize,
    string SearchTerm,
    string SortField,
    string SortDirection) : IRequest<PagedResult<IEnumerable<DepartmentDto>>>;