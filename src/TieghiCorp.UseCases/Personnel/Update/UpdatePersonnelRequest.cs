using MediatR;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Personnel.Update;

public sealed record UpdatePersonnelRequest(
    int Id,
    string Firstname,
    string Lastname,
    string Email,
    string JobTitle,
    int DepartmentId) : IRequest<Result>;