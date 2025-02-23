using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.GetById;

internal sealed class GetLocationByIdHandler(
    IQueryRepos<Core.Entities.Location> locationQuery) : IRequestHandler<GetLocationByIdRequest, Result<LocationDto>>
{
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;

    public async Task<Result<LocationDto>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
    {
        var location = await _locationQuery.GetByKeyAsync(l => l.Id == request.Id, cancellationToken);

        if (location is null)
        {
            return Result<LocationDto>.Failure(
                HttpError.NotFound(
                    entityName: "Location",
                    propertyValue: request.Id));
        }

        var locationDto = new LocationDto(location.Id, location.Name);

        return Result<LocationDto>.Success(locationDto);
    }
}