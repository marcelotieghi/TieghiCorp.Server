using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Create;

public sealed record CreatePersonnelRequest(
    string Firstname,
    string Lastname,
    string Email,
    string JobTitle,
    int DepartmentId) : IRequest<Result>;