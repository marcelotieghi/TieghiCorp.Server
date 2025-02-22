using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Delete;

public sealed record DeletePersonnelRequest(int Id) : IRequest<Result>;