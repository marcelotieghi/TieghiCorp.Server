using MediatR;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Core.Response;

namespace TieghiCorp.UseCases.Location.Delete;

internal sealed class DeleteLocationHandler(
    IUnitOfWork unitOfWork,
    IQueryRepos<Core.Entities.Location> locationQuery) : IRequestHandler<DeleteLocationRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQueryRepos<Core.Entities.Location> _locationQuery = locationQuery;

    public async Task<Result> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
    {
        if (!await _locationQuery.FindByKeyAsync(
            l => l.Id == request.Id,
            cancellationToken))
        {
            return Result.Failure(HttpError.NotFound("Location", request.Id));
        }

        await _unitOfWork.LocationCommand.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}