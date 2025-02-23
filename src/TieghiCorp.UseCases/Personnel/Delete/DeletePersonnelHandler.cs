using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Delete;

internal sealed class DeletePersonnelHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Personnel> personnelQuery) : IRequestHandler<DeletePersonnelRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelQuery = personnelQuery;

    public async Task<Result> Handle(DeletePersonnelRequest request, CancellationToken cancellationToken)
    {
        if (!await _personnelQuery.ExistByKeyAsync(p => p.Id == request.Id, cancellationToken))
        {
            return Result.Failure(
                HttpError.NotFound(
                    entityName: "Personnel",
                    propertyValue: request.Id));
        }

        await _unitOfWork.PersonnelCommand.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}