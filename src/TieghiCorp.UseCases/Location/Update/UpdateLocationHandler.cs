using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Update;

internal sealed class UpdateLocationHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Location> locationQuery) : IRequestHandler<UpdateLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;

    public async Task<Result> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var location = await _locationQuery.GetByKeyAsync(l => l.Id == request.Id, cancellationToken);

        if (await _locationQuery.ExistByKeyAsync(d => d.Name.ToLower().Trim() == request.Name.ToLower().Trim(), cancellationToken))
        {
            return Result.Failure(
                HttpError.Conflict(
                    entityName: "Location",
                    propertyName: "Name",
                    propertyValue: request.Name));
        }

        location.UpdateEntity(request);

        await _unitOfWork.LocationCommand.UpdateAsync(location, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}