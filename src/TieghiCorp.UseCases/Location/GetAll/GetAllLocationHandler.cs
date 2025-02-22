using MediatR;
using Microsoft.EntityFrameworkCore;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.GetAll;

internal sealed class GetAllLocationHandler(
    IQueryRepos<Core.Entities.Location> locationQuery) : IRequestHandler<GetAllLocationRequest, PagedResult<IEnumerable<LocationDto>>>
{
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;

    public async Task<PagedResult<IEnumerable<LocationDto>>> Handle(GetAllLocationRequest request, CancellationToken cancellationToken)
    {
        var locations = _locationQuery.GetAll();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            locations = locations.Where(l => EF.Functions.Like(l.Name.ToLower(), $"%{request.SearchTerm.ToLower()}%"));
        }

        locations = request.SortField.ToLower() switch
        {
            "name" => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? locations.OrderBy(l => l.Name)
                : locations.OrderByDescending(l => l.Name),
            _ => request.SortDirection.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                ? locations.OrderBy(l => l.Id)
                : locations.OrderByDescending(l => l.Id)
        };

        var totalCount = await locations.CountAsync(cancellationToken);

        var pagedLocations = await locations
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);


        var locationDtos = pagedLocations.Select(l => new LocationDto(l.Id, l.Name!));

        return PagedResult<IEnumerable<LocationDto>>.Success(locationDtos, totalCount, request.Page, request.PageSize);
    }
}