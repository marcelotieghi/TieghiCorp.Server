using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;
using TieghiCorp.UseCases.Location;

namespace TieghiCorp.UseCases.Department.GetById;

internal sealed class GetDepartmentByIdHandler(
    IQueryRepos<Core.Entities.Department> departmentQuery) : IRequestHandler<GetDepartmentByIdRequest, Result<DepartmentDto>>
{
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;

    public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
    {
        var department = await _departmentQuery.GetByKeyAsync(d => d.Id == request.Id, cancellationToken);

        if (!await _departmentQuery.ExistByKeyAsync(l => l.Id == request.Id, cancellationToken))
        {
            return Result<DepartmentDto>.Failure(
                HttpError.NotFound(
                    entityName: "Department",
                    propertyValue: request.Id));
        }

        var departmentDto = new DepartmentDto(
            department.Id,
            department.Name,
            new LocationDto(
                department.Location!.Id,
                department.Location.Name));

        return Result<DepartmentDto>.Success(departmentDto);
    }
}