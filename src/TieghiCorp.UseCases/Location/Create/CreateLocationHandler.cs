using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Create;

internal sealed class CreateLocationHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Location> locationQuery) : IRequestHandler<CreateLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;

    public async Task<Result> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
    {
        if (await _locationQuery.ExistByKeyAsync(d => d.Name.ToLower().Trim() == request.Name.ToLower().Trim(), cancellationToken))
        {
            return Result.Failure(
                HttpError.Conflict(
                    entityName: "Location",
                    propertyName: "Name",
                    propertyValue: request.Name));
        }

        var newLocation = new Core.Entities.Location(request.Name);

        await _unitOfWork.LocationCommand.CreateAsync(newLocation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}