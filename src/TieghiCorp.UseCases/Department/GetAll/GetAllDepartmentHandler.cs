using MediatR;
using Microsoft.EntityFrameworkCore;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;
using TieghiCorp.UseCases.Location;

namespace TieghiCorp.UseCases.Department.GetAll;

internal sealed class GetAllDepartmentHandler(
    IQueryRepos<Core.Entities.Department> departmentQuery) : IRequestHandler<GetAllDepartmentRequest, PagedResult<IEnumerable<DepartmentDto>>>
{
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;

    public async Task<PagedResult<IEnumerable<DepartmentDto>>> Handle(GetAllDepartmentRequest request, CancellationToken cancellationToken)
    {
        var departments = _departmentQuery.GetAll();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            departments = departments.Where(d =>
                EF.Functions.Like(d.Name.ToLower(), $"%{request.SearchTerm.ToLower()}%") ||
                EF.Functions.Like(d.Location!.Name.ToLower(), $"%{request.SearchTerm.ToLower()}%"));
        }

        departments = request.SortField.ToLower() switch
        {
            "name" => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? departments.OrderBy(d => d.Name)
                : departments.OrderByDescending(l => l.Name),

            "locationname" => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? departments.OrderBy(d => d.Location!.Name)
                : departments.OrderByDescending(d => d.Location!.Name),

            _ => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? departments.OrderBy(d => d.Id)
                : departments.OrderByDescending(l => l.Id)
        };

        var totalCount = await departments.CountAsync(cancellationToken);

        var pagedDepartments = await departments
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var departmentsDtos = pagedDepartments.Select(d =>
            new DepartmentDto(
                d.Id,
                d.Name,
                new LocationDto(
                    d.Location!.Id,
                    d.Location.Name)));

        return PagedResult<IEnumerable<DepartmentDto>>.Success(departmentsDtos, totalCount, request.Page, request.PageSize);
    }
}