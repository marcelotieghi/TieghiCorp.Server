using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.GetById;

public sealed record GetLocationByIdRequest(int Id) : IRequest<Result<LocationDto>>;