using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Update;

internal sealed class UpdatePersonnelHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Department> departmentocationQuery,
    IQueryRepos<Core.Entities.Personnel> personnelQuery) : IRequestHandler<UpdatePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Department> _departmenQuery = departmentocationQuery;
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelQuery = personnelQuery;

    public async Task<Result> Handle(UpdatePersonnelRequest request, CancellationToken cancellationToken)
    {
        if (!await _departmenQuery.ExistByKeyAsync(d => d.Id == request.DepartmentId, cancellationToken))
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Department",
                    propertyValue: request.DepartmentId));
        }

        var personnel = await _personnelQuery.GetByKeyAsync(p => p.Id == request.Id, cancellationToken);

        if (personnel is null)
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Personnel",
                    propertyValue: request.Id));
        }

        if (await _personnelQuery.ExistByKeyAsync(p => p.Email.ToLower().Trim() == request.Email.ToLower().Trim() && p.Id != request.Id, cancellationToken))
        {
            return Result.Failure(
                HttpError.Conflict(
                    entityName: "Personnel",
                    propertyName: "Email",
                    propertyValue: request.Email));
        }

        personnel.UpdateEntity(request);

        await _unitOfWork.PersonnelCommand.UpdateAsync(personnel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}