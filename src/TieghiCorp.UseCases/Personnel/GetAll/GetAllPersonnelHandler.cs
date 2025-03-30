using MediatR;
using Microsoft.EntityFrameworkCore;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;
using TieghiCorp.UseCases.Department;

namespace TieghiCorp.UseCases.Personnel.GetAll;

internal sealed class GetAllPersonnelHandler(
    IQueryRepos<Core.Entities.Personnel> personnelQuery) : IRequestHandler<GetAllPersonnelRequest, PagedResult<IEnumerable<PersonnelDto>>>
{
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelQuery = personnelQuery;

    public async Task<PagedResult<IEnumerable<PersonnelDto>>> Handle(GetAllPersonnelRequest request, CancellationToken cancellationToken)
    {
        var personnel = _personnelQuery.GetAll();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            personnel = personnel.Where(d =>
                EF.Functions.Like(d.FirstName.ToLower(), $"%{request.SearchTerm.ToLower()}%") ||
                EF.Functions.Like(d.LastName.ToLower(), $"%{request.SearchTerm.ToLower()}%") ||
                EF.Functions.Like(d.Email.ToLower(), $"%{request.SearchTerm.ToLower()}%") ||
                EF.Functions.Like(d.JobTitle.ToLower(), $"%{request.SearchTerm.ToLower()}%") ||
                EF.Functions.Like(d.Department!.Name.ToLower(), $"%{request.SearchTerm.ToLower()}%"));
        }

        personnel = request.SortField.ToLower() switch
        {
            "firstName" => request.SortDirection.ToLower() == "asc"
                ? personnel.OrderBy(d => d.FirstName)
                : personnel.OrderByDescending(d => d.FirstName),

            "lastname" => request.SortDirection.ToLower() == "asc"
                ? personnel.OrderBy(d => d.LastName)
                : personnel.OrderByDescending(d => d.LastName),

            "email" => request.SortDirection.ToLower() == "asc"
                ? personnel.OrderBy(d => d.Email)
                : personnel.OrderByDescending(d => d.Email),

            "jobtitle" => request.SortDirection.ToLower() == "asc"
                ? personnel.OrderBy(d => d.JobTitle)
                : personnel.OrderByDescending(d => d.JobTitle),

            "departmentname" => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? personnel.OrderBy(p => p.Department!.Name)
                : personnel.OrderByDescending(p => p.Department!.Name),

            _ => request.SortDirection.ToLower() == "asc"
                ? personnel.OrderBy(d => d.Id)
                : personnel.OrderByDescending(d => d.Id)
        };

        var totalCount = await personnel.CountAsync(cancellationToken);

        var pagedPersonnel = await personnel
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var personnelDto = pagedPersonnel.Select(p =>
            new PersonnelDto(
                p.Id,
                p.FirstName,
                p.LastName,
                p.Email,
                p.JobTitle,
                new DepartmentDto(
                    p.Department!.Id,
                    p.Department.Name)));

        return PagedResult<IEnumerable<PersonnelDto>>.Success(personnelDto, totalCount, request.Page, request.PageSize);
    }
}