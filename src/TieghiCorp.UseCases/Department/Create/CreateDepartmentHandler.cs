using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Create;

internal sealed class CreateDepartmentHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Location> locationQuery,
    IQueryRepos<Core.Entities.Department> departmentQuery) : IRequestHandler<CreateDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;

    public async Task<Result> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (!await _locationQuery.FindByKeyAsync(
            l => l.Id == request.LocationId,
            cancellationToken))
        {
            return Result.Failure(HttpError.NotFound("Location", request.LocationId));
        }

        if (await _departmentQuery.FindByKeyAsync(
            d => d.Name.ToLower().Trim() == request.Name.ToLower().Trim(),
            cancellationToken))
        {
            return Result.Failure(HttpError.Conflict("Department", "Name", request.Name));
        }

        var newDepartment = new Core.Entities.Department(request.Name, request.LocationId);

        await _unitOfWork.DepartmentCommand.CreateAsync(newDepartment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}