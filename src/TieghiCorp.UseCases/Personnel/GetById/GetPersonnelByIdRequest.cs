using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.GetById;

public sealed record GetPersonnelByIdRequest(int Id) : IRequest<Result<PersonnelDto>>;