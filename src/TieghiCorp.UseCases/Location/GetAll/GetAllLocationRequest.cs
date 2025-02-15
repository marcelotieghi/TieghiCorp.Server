using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.GetAll;

public sealed record GetAllLocationRequest(
    int Page,
    int PageSize,
    string SearchTerm,
    string SortField,
    string SortDirection) : IRequest<PagedResult<IEnumerable<LocationDto>>>;