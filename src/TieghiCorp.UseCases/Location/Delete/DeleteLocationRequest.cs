using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Delete;

public sealed record DeleteLocationRequest(int Id) : IRequest<Result>;