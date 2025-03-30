using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.GetAll;

public sealed record GetAllPersonnelRequest(
    int Page = 1,
    int PageSize = 25,
    string SearchTerm = "",
    string SortField = "id",
    string SortDirection = "asc") : IRequest<PagedResult<IEnumerable<PersonnelDto>>>;