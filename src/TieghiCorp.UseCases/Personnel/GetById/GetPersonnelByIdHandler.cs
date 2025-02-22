using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;
using TieghiCorp.UseCases.Department;

namespace TieghiCorp.UseCases.Personnel.GetById;

internal sealed class GetPersonnelByIdHandler(
    IQueryRepos<Core.Entities.Personnel> personnelRepos) : IRequestHandler<GetPersonnelByIdRequest, Result<PersonnelDto>>
{
    private readonly IQueryRepos<Core.Entities.Personnel> _personnelRepos = personnelRepos;

    public async Task<Result<PersonnelDto>> Handle(GetPersonnelByIdRequest request, CancellationToken cancellationToken)
    {
        var personnel = await _personnelRepos.GetByKeyAsync(p => p.Id == request.Id, cancellationToken);

        if (!await _personnelRepos.ExistByKeyAsync(p => p.Id == request.Id, cancellationToken))
        {
            return Result<PersonnelDto>.Failure(
                HttpError.NotFound(
                    entityName: "Personnel",
                    propertyValue: request.Id));
        }

        var personnelDto = new PersonnelDto(
            personnel.Id,
            personnel.FirstName,
            personnel.LastName,
            personnel.Email,
            personnel.JobTitle,
            new DepartmentDto(
                personnel.Department!.Id,
                personnel.Department.Name));

        return Result<PersonnelDto>.Success(personnelDto);
    }
}