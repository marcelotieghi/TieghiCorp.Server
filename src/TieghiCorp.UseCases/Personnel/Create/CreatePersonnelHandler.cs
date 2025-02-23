using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Create;

internal sealed class CreatePersonnelHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Department> departmentQuery,
    IQueryRepos<Core.Entities.Personnel> personnelQuery) : IRequestHandler<CreatePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Department> _departmentQuery = departmentQuery;
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelQuery = personnelQuery;

    public async Task<Result> Handle(CreatePersonnelRequest request, CancellationToken cancellationToken)
    {
        if (!await _departmentQuery.ExistByKeyAsync(d => d.Id == request.DepartmentId, cancellationToken))
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Department",
                    propertyValue: request.DepartmentId));
        }

        if (await _personnelQuery.ExistByKeyAsync(p => p.Email.ToLower().Trim() == request.Email.ToLower().Trim(), cancellationToken))
        {
            return Result.Failure(
                HttpError.Conflict(
                    entityName: "Personnel",
                    propertyName: "Email",
                    propertyValue: request.Email));
        }

        var newPersonnel = new Core.Entities.Personnel(
            request.Firstname,
            request.Lastname,
            request.Email,
            request.JobTitle,
            request.DepartmentId);

        await _unitOfWork.PersonnelCommand.CreateAsync(newPersonnel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}