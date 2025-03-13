using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.GetAll;

public sealed record GetAllLocationRequest(
    int Page = 1,
    int PageSize = 25,
    string SearchTerm = "",
    string SortField = "id",
    string SortDirection = "asc") : IRequest<PagedResult<IEnumerable<LocationDto>>>;