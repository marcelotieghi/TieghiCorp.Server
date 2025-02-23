using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Delete;

internal sealed class DeleteDepartmentHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Department> departmentQuery,
    IQueryRepos<Core.Entities.Personnel> personnelQuery) : IRequestHandler<DeleteDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelQuery = personnelQuery;

    public async Task<Result> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (!await _departmentQuery.ExistByKeyAsync(d => d.Id == request.Id, cancellationToken))
        {
            return Result.Failure(HttpError.NotFound("Department", request.Id));
        }

        if (await _personnelQuery.ExistByKeyAsync(d => d.DepartmentId == request.Id, cancellationToken))
        {
            return Result.Failure(HttpError.DependencyConflict("Department", "Personnel"));
        }

        await _unitOfWork.DepartmentCommand.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}