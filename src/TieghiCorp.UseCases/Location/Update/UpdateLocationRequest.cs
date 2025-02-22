using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Update;

public sealed record UpdateLocationRequest(int Id, string Name) : IRequest<Result>;