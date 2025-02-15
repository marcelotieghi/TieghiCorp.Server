using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Department.Delete;

internal sealed class DeleteDepartmentHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Department> departmentQuery) : IRequestHandler<DeleteDepartmentRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;

    public async Task<Result> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
    {
        if (!await _departmentQuery.FindByKeyAsync(
            d => d.Id == request.Id,
            cancellationToken))
        {
            return Result.Failure(HttpError.NotFound("Department", request.Id));
        }

        await _unitOfWork.DepartmentCommand.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}