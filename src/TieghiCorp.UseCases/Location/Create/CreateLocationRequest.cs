using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Create;

public sealed record CreateLocationRequest(string Name) : IRequest<Result>;