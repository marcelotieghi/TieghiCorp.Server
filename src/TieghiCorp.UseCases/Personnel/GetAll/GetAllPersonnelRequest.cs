using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.GetAll;

public sealed record GetAllPersonnelRequest(
    int Page,
    int PageSize,
    string SearchTerm,
    string SortField,
    string SortDirection) : IRequest<PagedResult<IEnumerable<PersonnelDto>>>;