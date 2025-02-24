using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Update;

internal sealed class UpdateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Location> locationQuery,
    IQueryRepos<Core.Entities.Department> departmentQuery) : IRequestHandler<UpdateDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;

    public async Task<Result> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (!await _locationQuery.ExistByKeyAsync(l => l.Id == request.LocationId, cancellationToken))
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Location",
                    propertyValue: request.LocationId));
        }

        var department = await _departmentQuery.GetByKeyAsync(d => d.Id == request.Id, cancellationToken);

        if (department is null)
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Department",
                    propertyValue: request.Id));
        }

        if (await _departmentQuery.ExistByKeyAsync(d => d.Name.ToLower().Trim() == request.Name.ToLower().Trim() && d.Id != request.Id, cancellationToken))
        {
            return Result.Failure(
                HttpError.Conflict(
                    entityName: "Department",
                    propertyName: "Name",
                    propertyValue: request.Name));
        }

        department.UpdateEntity(request);

        await _unitOfWork.DepartmentCommand.UpdateAsync(department, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}