using System.Text.Json.Serialization;

namespace TieghiCorp.Core.Response;

public class PagedResult<TData> : Result<TData>
{
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    [JsonConstructor]
    private PagedResult(TData data, int totalCount, int currentPage, int pageSize) : base(data)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    private PagedResult(HttpError error) : base(error)
    {
        TotalCount = 0;
        CurrentPage = 1;
        PageSize = 25;
    }

    public static PagedResult<TData> Success(
        TData data,
        int totalCount,
        int currentPage = 1,
        int pageSize = 25) => new(data, totalCount, currentPage, pageSize);

    public static new PagedResult<TData> Failure(HttpError error) => new(error);
}